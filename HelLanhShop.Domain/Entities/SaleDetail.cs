using HelLanhShop.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelLanhShop.Domain.Entities
{
    public partial class SaleDetail : BaseEntity
    {
        public int Id { get; set; }
        public int SaleId { get; set; }
        public int ProductId { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal SubTotal => Quantity * UnitPrice;

        // Navigation
        public Sale Sale { get; set; } = null!;
        public Product Product { get; set; } = null!;
    }
}
