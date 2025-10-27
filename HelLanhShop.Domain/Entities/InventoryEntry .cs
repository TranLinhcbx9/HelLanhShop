using HelLanhShop.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelLanhShop.Domain.Entities
{
    // Nhap hang
    public partial class InventoryEntry : BaseEntity
    {
        public int Id { get; set; }
        public DateTime EntryDate { get; set; } = DateTime.UtcNow;
        public string? Note { get; set; }

        // FK đến Supplier
        public int? EmployeeId { get; set; }
        public Employee? Employee { get; set; }
        public int? SupplierId { get; set; }
        public Supplier? Supplier { get; set; }

        // Navigation
        public ICollection<InventoryEntryDetail>? InventoryEntryDetails { get; set; }
    }
}
