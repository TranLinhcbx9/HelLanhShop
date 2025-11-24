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

        public AuthService(IUnitOfWork unitOfWork, IJwtTokenService jwtTokenService, IOptions<JwtSettings> jwtOption)
        {
            _unitOfWork = unitOfWork;
            _jwtTokenService = jwtTokenService;
            _jwtSettings = jwtOption.Value;
        }
        public async Task<Result<LoginResponseDto>> LoginAsync(LoginRequestDto requestDto)
        {
            var user = await _unitOfWork.Users.GetByUsernameAsync(requestDto.UserName);
            if (user == null) return Result<LoginResponseDto>.Failure("User not found");

            var verified = VerifyPassword(requestDto.Password, user.PasswordHash);
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
        private bool VerifyPassword(string plain, string hash)
        {
            return plain == hash; // demo 
            //return BCrypt.Net.BCrypt.Verify(plain, hash);
        }
    }
}
