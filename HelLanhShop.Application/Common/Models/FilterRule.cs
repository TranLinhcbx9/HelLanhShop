using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelLanhShop.Application.Common.Models
{
    public class FilterRule
    {
        public string Field { get; set; } = default!;
        public string Operator { get; set; } = "contains";
        public string? Value { get; set; }
        public string? Value2 { get; set; } // between
    }
}
