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
using System.IdentityModel.Tokens.Jwt;
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
            var user = await _unitOfWork.Users.GetByUserNameOrEmailAsync(requestDto.UserNameOrEmail);
            if (user == null) return Result<LoginResponseDto>.Failure("User not found");

            var verified = _passwordHasher.Verify(requestDto.Password, user.PasswordHash);
            if (!verified) return Result<LoginResponseDto>.Failure("Invalid password");

            var userAuth = await GetUserAuthDataAsync(requestDto.UserNameOrEmail);
            if (userAuth == null) return Result<LoginResponseDto>.Failure("Cannot load authorization data");

            if (!userAuth.Roles.Any())
                return Result<LoginResponseDto>.Failure("User has no roles assigned.");
            //Generate JWT
            var token = _jwtTokenService.GenerateAccessToken(user, userAuth.Roles, userAuth.Permissions);
            var tokenObj = new JwtSecurityTokenHandler().ReadJwtToken(token);
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
                Expiration = tokenObj.ValidTo,
                RefreshToken = refreshToken.Token
            };
            return Result<LoginResponseDto>.Success(responseDto);
        }
        public async Task<Result<LoginResponseDto>> RefreshTokenAsync(string refreshToken)
        {
            var rToken = await _unitOfWork.RefreshTokens.GetByTokenAsync(refreshToken);
            if (rToken == null || rToken.ExpiryDate < DateTime.UtcNow) return Result<LoginResponseDto>.Failure("Invalid or expired refresh newAccessTokem");

            var user = await _unitOfWork.Users.GetByIdAsync(rToken.UserId);
            if (user == null) return Result<LoginResponseDto>.Failure("User not found");

            var userAuth = await GetUserAuthDataAsync(user.UserName);
            if (userAuth == null) return Result<LoginResponseDto>.Failure("Cannot load authorization data");

            if (!userAuth.Roles.Any())
                return Result<LoginResponseDto>.Failure("User has no roles assigned.");

            var newAccessTokem = _jwtTokenService.GenerateAccessToken(user,userAuth.Roles, userAuth.Permissions);

            //rotate refresh newAccessTokem
            rToken.Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
            rToken.ExpiryDate = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenDays);
            await _unitOfWork.SaveChangesAsync();
            var newTokenObj = new JwtSecurityTokenHandler().ReadJwtToken(newAccessTokem);
            var responseDto = new LoginResponseDto
            {
                Token = newAccessTokem,
                Expiration = newTokenObj.ValidTo,
                RefreshToken = rToken.Token
            };
            return Result<LoginResponseDto>.Success(responseDto);
        }
        public async Task<Result<RegisterResponseDto>> RegisterAsync(RegisterRequestDto requestDto)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            { 
                var isExistingUser = await _unitOfWork.Users.AnyAsync(u => (u.UserName == requestDto.UserName || ((!String.IsNullOrEmpty(requestDto.Email)) && u.Email == requestDto.Email)));
                if (isExistingUser)
                {
                    await _unitOfWork.RollbackTransactionAsync();
                    return Result<RegisterResponseDto>.Failure("Username or Email already exists");
                }

                
                //add user
                var user = new User
                {
                    UserName = requestDto.UserName,
                    Email = requestDto.Email,
                    PasswordHash = _passwordHasher.Hash(requestDto.Password),
                };

                //add customer
                var customer = new Customer
                {
                    User = user,
                    Name = requestDto.Name!,
                    Phone = requestDto.Phone,
                    Address = requestDto.Address
                };

                var customerRole = await _unitOfWork.Roles.GetByNameAsync("Customer");
                if (customerRole == null)
                {
                    await _unitOfWork.RollbackTransactionAsync();
                    return Result<RegisterResponseDto>.Failure("Customer role not found");
                }
                var userRole = new UserRole
                {
                    User = user,
                    Role = customerRole
                };
                // add refresh token
                var refreshToken = new RefreshToken
                {
                    Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                    ExpiryDate = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenDays),
                    User = user
                };

                await _unitOfWork.Users.AddAsync(user);
                await _unitOfWork.Customers.AddAsync(customer);
                await _unitOfWork.UserRoles.AddAsync(userRole);
                await _unitOfWork.RefreshTokens.AddAsync(refreshToken);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransactionAsync();

                var userAuth = await GetUserAuthDataAsync(requestDto.UserName);
                if (userAuth == null || !userAuth.Roles.Any())
                    return Result<RegisterResponseDto>.Failure("User has no roles assigned.");

                //Auto generate JWT newAccessToken after register
                var token = _jwtTokenService.GenerateAccessToken(user, userAuth.Roles, userAuth.Permissions);
                var tokenObj = new JwtSecurityTokenHandler().ReadJwtToken(token);

                var responseDto = new RegisterResponseDto
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    Name = customer.Name,
                    Phone = customer.Phone,
                    Address = customer.Address,
                    Token = token,
                    Expiration = tokenObj.ValidTo,
                    RefreshToken = refreshToken.Token

                };
                return Result<RegisterResponseDto>.Success(responseDto);
            }
            catch (Exception)
            { 
                await _unitOfWork.RollbackTransactionAsync();
                throw; 
            }
        }
        public async Task<UserAuthDataDto?> GetUserAuthDataAsync(string usernameOrEmail)
        {
            var user = await _unitOfWork.Users.GetUserAuthGraphByUserNameOrEmailAsync(usernameOrEmail);
            if (user == null) return null;
            var roleNames = user.UserRoles.Select(ur => ur.Role.Name).ToList();
            var directPermissions = user.UserPermissions.Select(up => up.Permission.Name);
            var rolePermissions = user.UserRoles.SelectMany(ur => ur.Role.RolePermissions).Select(rp => rp.Permission.Name);
            var allPermissions = directPermissions.Union(rolePermissions).Distinct().ToList();

            return new UserAuthDataDto        
            {
                UserId = user.Id,
                UserName = user.UserName,
                Roles = roleNames,
                Permissions = allPermissions
            };
        }
    }
}
