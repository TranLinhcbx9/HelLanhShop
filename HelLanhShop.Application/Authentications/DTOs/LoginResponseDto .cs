using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelLanhShop.Application.Authentications.DTOs
{
    public class LoginResponseDto
    {
        public string Token { get; set; } = null!; 
        public DateTime Expiration { get; set; }
        public string RefreshToken { get; set; } = null!;
    }
}
