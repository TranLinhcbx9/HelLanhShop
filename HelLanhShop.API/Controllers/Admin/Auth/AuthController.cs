using HelLanhShop.API.Common;
using HelLanhShop.API.Common.ApiResponses;
using HelLanhShop.Application.Authentications.DTOs;
using HelLanhShop.Application.Authentications.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net.Sockets;

namespace HelLanhShop.API.Controllers.Admin.Auth
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<ApiResponse<LoginResponseDto>>> Login(LoginRequestDto dto)
        {
            var result = await _authService.LoginAsync(dto);
            return FromResult(result);
            
        }
        [HttpPost("refresh")]
        public async Task<ActionResult<ApiResponse<LoginResponseDto>>> Refresh([FromBody] RefreshRequestDto refreshRequest)
        {
            var result = await _authService.RefreshTokenAsync(refreshRequest.RefreshToken);
            return FromResult(result);
        }
        [HttpPost("register")]
        public async Task<ActionResult<ApiResponse<RegisterResponseDto>>> Register(RegisterRequestDto dto)
        {
            var result = await _authService.RegisterAsync(dto);
            
            return FromResult(result);
        }
    }
}
