using HelLanhShop.Application.Common.Enums;

namespace HelLanhShop.API.Common.Extensions
{
    public static class ErrorTypeExtensions
    {
        public static int ToHttpStatusCode(this ErrorType errorType) => errorType switch
        {
            ErrorType.Validation => 400,
            ErrorType.NotFound => 404,
            ErrorType.Conflict => 409,
            ErrorType.Unauthorized => 401,
            ErrorType.Forbidden => 403,
            _ => 500
        };
    }
}
