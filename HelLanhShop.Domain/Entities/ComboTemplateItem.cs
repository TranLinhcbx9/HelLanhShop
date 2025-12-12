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

        public int ComboTemplateId { get; set; }
        public ComboTemplate ComboTemplate { get; set; } = null!;

        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;

        public decimal WeightGram { get; set; }    // Số gram cho sản phẩm này trong combo
        public decimal? Ratio { get; set; } // tỉ trọng, vd: 0.4 (40%)
    }
}
