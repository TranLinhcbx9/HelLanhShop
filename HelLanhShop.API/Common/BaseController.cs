using HelLanhShop.API.Common.ApiResponses;
using HelLanhShop.API.Common.Extensions;
using HelLanhShop.Application.Common.Enums;
using HelLanhShop.Application.Common.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HelLanhShop.API.Common
{
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        /// Convert Result<T> → ApiResponse<T> → ActionResult<T>
        protected ActionResult<ApiResponse<T>> FromResult<T>(
        Result<T> result)
        {
            if (result.IsSuccess)
            {
                return Ok(ApiResponse<T>.Ok(result.Data));
            }

            return StatusCode(result.ErrorType.ToHttpStatusCode(), ApiResponse<T>.Fail(result.Error ?? "Error", default, result.ErrorType, result.ErrorCode));
        }
        protected ActionResult<ApiResponse> FromResult(Result result)
        {
            if (result.IsSuccess)
            {
                return Ok(ApiResponse.Ok());
            }
            return StatusCode(result.ErrorType.ToHttpStatusCode(), ApiResponse.Fail(result.Error ?? "Error", result.ErrorType, result.ErrorCode));
        }
        protected ActionResult<PagedResponse<T>> FromPaged<T>(Result<PagedResult<T>> result)
        {
            if (!result.IsSuccess)
                return StatusCode(
                    result.ErrorType.ToHttpStatusCode(),
                    PagedResponse<T>.Fail(result.Error ?? "Error", default, result.ErrorType, result.ErrorCode)
                );

            var p = result.Data!;

            var response = PagedResponse<T>.Create(
                p.Data ?? Enumerable.Empty<T>(),
                p.PageIndex,
                p.PageSize,
                p.TotalItems,
                "Success",
                ErrorType.None,
                ErrorCode.NONE
            );

            return Ok(response);
        }

        // ===== ErrorType → HTTP StatusCode =====
    }
}
