using HelLanhShop.API.Common.ApiResponse;
using HelLanhShop.Application.Authentications.DTOs;
using HelLanhShop.Application.Authentications.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HelLanhShop.API.Controllers.Auth
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")] 
        public async Task<ActionResult<LoginResponseDto>> Login(LoginRequestDto dto)
        {
            var result = await _authService.LoginAsync(dto);
            if (!result.IsSuccess)
            {
                return BadRequest(ApiResponse<LoginResponseDto>.Fail(result.Error!));
            }
            return Ok(ApiResponse<LoginResponseDto>.Ok(result.Data!));
        }
        [HttpPost("refresh")]
        public async Task<ActionResult<LoginResponseDto>> Refresh([FromBody] string refreshToken)
        {
            var result = await _authService.RefreshTokenAsync(refreshToken);
            if (!result.IsSuccess) return BadRequest(ApiResponse<LoginResponseDto>.Fail(result.Error!));
            return Ok(ApiResponse<LoginResponseDto>.Ok(result.Data!));
        }
    }
}
