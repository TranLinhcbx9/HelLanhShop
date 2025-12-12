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
        public int? CustomerId { get; set; }
        public int? EmployeeId { get; set; }
        public DateTime SaleDate { get; set; }
        public decimal TotalAmount { get; set; }

        // Navigation
        public Customer Customer { get; set; } = null!;
        public Employee Employee { get; set; } = null!;
        public ICollection<SaleDetail> SaleDetails { get; set; } = null!;
    }
}
