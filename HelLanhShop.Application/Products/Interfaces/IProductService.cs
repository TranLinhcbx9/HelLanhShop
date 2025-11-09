using HelLanhShop.Application.Products.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelLanhShop.Application.Products.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllAsync();
        Task<ProductDto?> GetByIdAsync(int id);
        Task<CreateProduct> CreateAsync(CreateProduct createProduct);
        Task<UpdateProduct> UpdateAsync(UpdateProduct updateProduct);
        Task<ProductDto> DeleteAsync(int id);
    }
}
