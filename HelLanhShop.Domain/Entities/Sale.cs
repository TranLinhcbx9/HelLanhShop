using HelLanhShop.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelLanhShop.Domain.Entities
{
    public partial class Sale : BaseEntity
    {
        public int Id { get; set; }
        public int ProductId { get; set; }       // FK Product
        public decimal Quantity { get; set; }    // số lượng bán
        public decimal SalePrice { get; set; }   // giá bán lần này
        public DateTime? SaleDate { get; set; }

        public virtual Product? Product { get; set; }
    }
}
