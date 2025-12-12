using HelLanhShop.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelLanhShop.Domain.Entities
{
    public partial class Supplier : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;      // Tên nhà cung cấp
        public string? Phone { get; set; }
        public string? Address { get; set; }

        // Navigation
        public ICollection<InventoryEntry> InventoryEntries { get; set; } = null!;
    }
}
