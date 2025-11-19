using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelLanhShop.Application.Common.Models
{
    public class BaseFilter
    {
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public List<FilterRule>? Rules { get; set; }

        public string? SortField { get; set; }
        public string SortDir { get; set; } = "asc";
    }
}
