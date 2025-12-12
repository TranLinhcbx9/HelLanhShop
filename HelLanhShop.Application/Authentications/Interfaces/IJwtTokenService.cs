using HelLanhShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HelLanhShop.Application.Authentications.Interfaces
{
    public interface IJwtTokenService
    {
        string GenerateAccessToken(User user, List<string> roles, List<string> permissions);
        //ClaimsPrincipal? ValidateAccessToken(string token);

    }
}
