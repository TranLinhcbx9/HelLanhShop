using HelLanhShop.Application.Common.Models;
using HelLanhShop.Application.Products.DTOs;
using HelLanhShop.Application.Products.Filters;
using HelLanhShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        Task<PagedResult<ProductDto>> SearchAsync(ProductFilter filter);
    }
}
