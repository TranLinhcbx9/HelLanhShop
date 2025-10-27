using HelLanhShop.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelLanhShop.Domain.Entities
{
    public partial class InventoryEntry : BaseEntity
    {
        public int Id { get; set; }
        public int ProductId { get; set; }       // FK Product
        public decimal Quantity { get; set; }    // số lượng nhập
        public decimal Cost { get; set; }        // giá nhập lần này
        public DateTime EntryDate { get; set; }

        // Nếu muốn thêm nguồn hàng
        public int? SupplierId { get; set; }
        public virtual Supplier? Supplier { get; set; }

        // Navigation property
        public virtual Product? Product { get; set; }
    }
}
