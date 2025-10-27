using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelLanhShop.Domain.Common;

namespace HelLanhShop.Domain.Entities
{
    public partial class Product : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;   // tên trái cây
        public decimal CostPrice { get; set; }             // giá nhập
        public decimal UnitPrice { get; set; }             // giá bán lẻ (cho admin / khách xem sau)
        public decimal? Stock { get; set; }                // tồn kho
        public string Unit { get; set; } = "gram";        // đơn vị

        // Navigation properties
        public virtual ICollection<ComboTemplateItem>? ComboItems { get; set; }
        public virtual ICollection<InventoryEntry>? InventoryEntries { get; set; }
        public virtual ICollection<Sale>? Sales { get; set; }

    }
}
