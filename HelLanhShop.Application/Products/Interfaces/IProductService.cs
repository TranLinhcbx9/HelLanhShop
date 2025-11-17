using HelLanhShop.Application.Products.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelLanhShop.Application.Common.Models;

namespace HelLanhShop.Application.Products.Interfaces
{
    public interface IProductService
    {
        Task<Result<List<ProductDto>>> GetAllAsync();
        Task<Result<PagedResult<ProductDto>>> GetAllPagingAsync(int pageIndex, int pageSize);
        Task<Result<ProductDto?>> GetByIdAsync(int id);
        Task<Result<CreateProductDto>> CreateAsync(CreateProductDto createProduct);
        Task<Result<UpdateProductDto>> UpdateAsync(UpdateProductDto updateProduct);
        Task<Result<ProductDto>> DeleteAsync(int id);
    }
}
