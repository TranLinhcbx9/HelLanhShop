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
        public string? Name { get; set; } = null!;
        public string? Phone { get; set; }
        public string? Address { get; set; }
        //auto gen token after register
        public string? JwtToken { get; set; }
    }
}
