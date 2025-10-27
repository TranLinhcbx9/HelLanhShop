using HelLanhShop.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelLanhShop.Domain.Entities
{
    public partial class ComboTemplate :BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;   // Tên combo
        public string? Description { get; set; }           // Mô tả
        public decimal Price { get; set; }                 // Giá bán combo
        public decimal? TotalWeight { get; set; }          // Tổng khối lượng (nếu có)

        // Navigation
        public ICollection<ComboTemplateItem>? ComboTemplateItems { get; set; }
    }
}
