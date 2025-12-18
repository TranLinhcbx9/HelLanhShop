using HelLanhShop.API.Common.ApiResponses;
using HelLanhShop.Application.Common.Enums;
using System.Text.Json;
using Volo.Abp;

namespace HelLanhShop.API.Middlewares
{
    public sealed class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleAsync(context, ex);
            }
        }

        private async Task HandleAsync(HttpContext context, Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception | Path: {Path}", context.Request.Path);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            var response = ApiResponse.Fail(
                message: _env.IsDevelopment() ? ex.Message : "Internal server error",
                errorType: ErrorType.None,
                errorCode: ErrorCode.INTERNAL_ERROR
            );

            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
