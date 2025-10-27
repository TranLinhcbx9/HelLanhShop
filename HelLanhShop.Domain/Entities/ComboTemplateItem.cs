using HelLanhShop.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelLanhShop.Domain.Entities
{
    public partial class ComboTemplateItem : BaseEntity
    {
        public int Id { get; set; }
        public int ComboTemplateId { get; set; }  // FK ComboTemplate
        public int ProductId { get; set; }        // FK Product
        public int? Ratio { get; set; }            // tỉ lệ để tính gram

        // Navigation properties
        public virtual ComboTemplate? ComboTemplate { get; set; }
        public virtual Product? Product { get; set; }
    }
}
