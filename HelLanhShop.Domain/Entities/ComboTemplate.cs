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
        public string Name { get; set; } = string.Empty;  // tên combo

        // Combo có nhiều item (tỉ lệ từng loại)
        public virtual ICollection<ComboTemplateItem>? Items { get; set; }
    }
}
