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
        public string Name { get; set; } = string.Empty;   // Tên trái cây
        public decimal CostPrice { get; set; }             // Giá nhập trung bình
        public decimal UnitPrice { get; set; }             // Giá bán
        public decimal? Stock { get; set; }                // Tồn kho hiện tại
        public string Unit { get; set; } = "gram";         // Đơn vị tính
        public string? Category { get; set; }
        // Navigation
        public ICollection<ComboTemplateItem>? ComboTemplateItems { get; set; }
        public ICollection<InventoryEntryDetail>? InventoryEntryDetails { get; set; }
        public ICollection<SaleDetail>? SaleDetails { get; set; }

    }
}
