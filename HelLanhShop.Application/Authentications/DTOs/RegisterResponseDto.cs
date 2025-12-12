using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelLanhShop.Application.Authentications.DTOs
{
    public class RegisterResponseDto
    {
        public int UserId { get; set; }
        public required string UserName { get; set; }
        public string Email { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Address { get; set; } = null!;
        //auto gen token after register
        public string Token { get; set; } = null!;
        public DateTime Expiration { get; set; }       // Thời hạn access token
        public string RefreshToken { get; set; } = null!; // Refresh token
    }
}
