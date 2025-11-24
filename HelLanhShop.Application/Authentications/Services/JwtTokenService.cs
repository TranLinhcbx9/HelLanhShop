using HelLanhShop.Application.Authentications.Interfaces;
using HelLanhShop.Application.Authentications.Models;
using HelLanhShop.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HelLanhShop.Application.Authentications.Services
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly IConfiguration _config;
        private readonly JwtSettings _jwtSetting;
        public JwtTokenService(IConfiguration config, IOptions<JwtSettings> jwtOption)
        {
            _config = config;
            _jwtSetting = jwtOption.Value;
        }

        public string GenerateAccessToken(User user)
        {
            //payload
            var claims = new List<Claim>
            {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName)
            };
            if (!user.Roles.Any())
            {
                throw new UnauthorizedAccessException("User has no roles assigned.");
            }
            foreach (var role in user.Roles)
                claims.Add(new Claim(ClaimTypes.Role, role)); 


            // Tạo signature
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSetting.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Tạo token
            var token = new JwtSecurityToken(
                issuer: _jwtSetting.Issuer,
                audience: _jwtSetting.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(60),
                signingCredentials: creds
            );

            // 4️ Trả về token string
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
