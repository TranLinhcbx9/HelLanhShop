using HelLanhShop.Application.Authentications.DTOs;
using HelLanhShop.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelLanhShop.Application.Authentications.Interfaces
{
    public interface IAuthService
    {
        Task<Result<LoginResponseDto>> LoginAsync(LoginRequestDto loginRequestDto);
        Task<Result<LoginResponseDto>> RefreshTokenAsync(string refreshToken);
        Task<Result<RegisterResponseDto>> RegisterAsync(RegisterRequestDto registerRequestDto);
        Task<UserAuthDataDto?> GetUserAuthDataAsync(string usernameOrEmail);

    }
}
