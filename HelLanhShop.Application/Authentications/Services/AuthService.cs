using AutoMapper;
using HelLanhShop.Application.Authentications.DTOs;
using HelLanhShop.Application.Authentications.Interfaces;
using HelLanhShop.Application.Authentications.Models;
using HelLanhShop.Application.Common.Interfaces;
using HelLanhShop.Application.Common.Models;
using HelLanhShop.Domain.Entities;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HelLanhShop.Application.Authentications.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly JwtSettings _jwtSettings;
        public readonly IPasswordHasher _passwordHasher;

        public AuthService(IUnitOfWork unitOfWork, IJwtTokenService jwtTokenService, IOptions<JwtSettings> jwtOption, IPasswordHasher passwordHasher)
        {
            _unitOfWork = unitOfWork;
            _jwtTokenService = jwtTokenService;
            _jwtSettings = jwtOption.Value;
            _passwordHasher = passwordHasher;
        }
        public async Task<Result<LoginResponseDto>> LoginAsync(LoginRequestDto requestDto)
        {
            var user = await _unitOfWork.Users.GetByUsernameAsync(requestDto.UserName);
            if (user == null) return Result<LoginResponseDto>.Failure("User not found");

            var verified = _passwordHasher.Verify(requestDto.Password, user.PasswordHash);
            if (!verified) return Result<LoginResponseDto>.Failure("Invalid password");

            //Generate JWT
            var token = _jwtTokenService.GenerateAccessToken(user);

            //Generate Refresh Token
            var refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                ExpiryDate = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenDays),
                UserId = user.Id
            };
            await _unitOfWork.RefreshTokens.AddAsync(refreshToken);
            await _unitOfWork.SaveChangesAsync();

            var responseDto = new LoginResponseDto
            {
                Token = token,
                Expiration = DateTime.UtcNow.AddMinutes(_jwtSettings.AccessTokenMinutes),
                RefreshToken = refreshToken.Token
            };
            return Result<LoginResponseDto>.Success(responseDto);
        }
        public async Task<Result<LoginResponseDto>> RefreshTokenAsync(string refreshToken)
        {
            var rToken = await _unitOfWork.RefreshTokens.GetByTokenAsync(refreshToken);
            if (rToken == null || rToken.ExpiryDate < DateTime.UtcNow) return Result<LoginResponseDto>.Failure("Invalid or expired refresh token");

            var user = await _unitOfWork.Users.GetByIdAsync(rToken.UserId);
            if (user == null) return Result<LoginResponseDto>.Failure("User not found");
            var token = _jwtTokenService.GenerateAccessToken(user);

            //rotate refresh token
            rToken.Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
            rToken.ExpiryDate = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenDays);
            await _unitOfWork.SaveChangesAsync();
            var responseDto = new LoginResponseDto
            {
                Token = token,
                Expiration = DateTime.UtcNow.AddMinutes(_jwtSettings.AccessTokenMinutes),
                RefreshToken = rToken.Token
            };
            return Result<LoginResponseDto>.Success(responseDto);
        }
        public async Task<Result<RegisterResponseDto>> RegisterAsync(RegisterRequestDto requestDto)
        {
            var isExistingUser = await _unitOfWork.Users.AnyAsync(u => u.UserName == requestDto.UserName || u.Email == requestDto.Email);
            if (isExistingUser) return Result<RegisterResponseDto>.Failure("Username or Email already exists");
            var user = new User
            {
                UserName = requestDto.UserName,
                Email = requestDto.Email,
                PasswordHash = _passwordHasher.Hash(requestDto.Password)
            };
            await _unitOfWork.Users.AddAsync(user);
            await _unitOfWork.SaveChangesAsync();
            var customer = new Customer
            {
                UserId = user.Id,
                Name = requestDto.Name!,
                Phone = requestDto.Phone,
                Address = requestDto.Address
            };
            await _unitOfWork.Customers.AddAsync(customer);
            await _unitOfWork.SaveChangesAsync();

            //Auto generate JWT token after register
            var token = _jwtTokenService.GenerateAccessToken(user);
            var responseDto = new RegisterResponseDto
            {
                UserId = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Name = customer.Name,
                Phone = customer.Phone,
                Address = customer.Address,
                JwtToken = token
            };
            return Result<RegisterResponseDto>.Success(responseDto);
        }
        
    }
}
