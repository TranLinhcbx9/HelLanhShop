using HelLanhShop.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelLanhShop.Domain.Entities
{
    public class Customer : BaseEntity
    {
        public int Id { get; set; }                 
        [ForeignKey("User")]
        public int UserId { get; set; }             
        public User? User { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public string? Address { get; set; }

        // Navigation
        public ICollection<Sale>? Sales { get; set; }
    }
}
