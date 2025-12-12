using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelLanhShop.Application.Authentications.DTOs
{
    public class UserAuthDataDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = null!;

        public List<string> Roles { get; set; } = new();
        public List<string> Permissions { get; set; } = new();
    }
}
