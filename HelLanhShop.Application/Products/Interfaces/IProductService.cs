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
        Task<Result<List<ProductAdminDto>>> GetAllAsync();
        Task<Result<PagedResult<ProductAdminDto>>> GetAllPagingAsync(RequestPagingProduct request);
        Task<Result<ProductAdminDto?>> GetByIdAsync(int id);
        Task<Result<CreateProductDto>> CreateAsync(CreateProductDto createProduct);
        Task<Result<UpdateProductDto>> UpdateAsync(UpdateProductDto updateProduct);
        Task<Result<ProductAdminDto>> DeleteAsync(int id);
        Task<Result<PagedResult<ProductAdminDto>>> SearchAsync(ProductFilter filter);
    }
}
