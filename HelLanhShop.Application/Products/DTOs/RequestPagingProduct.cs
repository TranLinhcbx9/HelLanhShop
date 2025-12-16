using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelLanhShop.Application.Products.DTOs
{
    public  class RequestPagingProduct
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}
