using HelLanhShop.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;

namespace HelLanhShop.Application.Common.Services
{
    public class CurrentUser : ICurrentUser
    {
        private readonly IHttpContextAccessor _httpContext;
        public CurrentUser(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }
        public int? UserId
        {
             get
            {
                var userIdClaim = _httpContext.HttpContext?.User?.FindFirst("userId")?.Value;
                if (int.TryParse(userIdClaim, out int userId))
                {
                    return userId;
                }
                return null;
            }
        }
    }
}
