using HelLanhShop.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelLanhShop.Domain.Entities
{
    public class Employee : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Role { get; set; } = "Staff"; // hoặc Admin
        public string? Username { get; set; }
        public string? PasswordHash { get; set; }

        // Navigation
        public ICollection<Sale>? Sales { get; set; }
        public ICollection<InventoryEntry>? InventoryEntries { get; set; }
    }
}
