using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelLanhShop.Application.Authentications.DTOs
{
    public class RegisterRequestDto
    {
        [Required(ErrorMessage = "UserName is required")]
        public required string UserName { get; set; }
        public string Email { get; set; } = null!;
        [Required(ErrorMessage = "Password is required")]
        public required string Password { get; set; }
        public string Name { get; set; } = null!; //FullName
        public string Phone { get; set; } = null!;
        public string Address { get; set; } = null!;
    }
}
