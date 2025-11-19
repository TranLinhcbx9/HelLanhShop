using AutoMapper;
using HelLanhShop.Application.Common.Interfaces;
using HelLanhShop.Application.Common.Models;
using HelLanhShop.Application.Common.Services;
using HelLanhShop.Application.Products.DTOs;
using HelLanhShop.Application.Products.Filters;
using HelLanhShop.Application.Products.Interfaces;
using HelLanhShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelLanhShop.Application.Products.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IGenericService<Product> _genericService;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper, IGenericService<Product> baseService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _genericService = baseService;
        }
        public async Task<Result<List<ProductDto>>> GetAllAsync()
        {
            try
            {
                var products = await _unitOfWork.Products.GetAllAsync(); // trả List<Product>
                var dtos = _mapper.Map<List<ProductDto>>(products ?? new List<Product>());
                return Result<List<ProductDto>>.Success(dtos);
            }
            catch (Exception ex)
            {
                return Result<List<ProductDto>>.Failure(ex.Message);
            }
        }
        
        public async Task<Result<PagedResult<ProductDto>>> GetAllPagingAsync(int pageIndex, int pageSize)
        {
            try
            {
                var query = _unitOfWork.Products.Query();
                var pagedProducts = await _unitOfWork.Products.GetPagedAsync(query, pageIndex, pageSize); 
                var dtos = _mapper.Map<List<ProductDto>>(pagedProducts.Data);
                var pagedDto = PagedResult<ProductDto>.Success(dtos, pagedProducts.PageIndex, pagedProducts.PageSize, pagedProducts.TotalItems);
                return Result<PagedResult<ProductDto>>.Success(pagedDto);
            }
            catch (Exception ex)
            {
                return Result<PagedResult<ProductDto>>.Failure(ex.Message);
            }
        }

        public async Task<Result<CreateProductDto>> CreateAsync(CreateProductDto createProduct)
        {
            try
            {
                var product = _mapper.Map<Product>(createProduct);
                await _unitOfWork.Products.AddAsync(product);
                await _unitOfWork.SaveChangesAsync();
                var dto = _mapper.Map<CreateProductDto>(product);
                return  Result<CreateProductDto>.Success(dto);
            }
            catch (Exception ex)
            {
                return Result<CreateProductDto>.Failure(ex.Message);
            }
        }

        public async Task<Result<ProductDto>> DeleteAsync(int id)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(id);
            if (product == null)
            {
                return Result<ProductDto>.Failure("Product Not Found");
            }
            _unitOfWork.Products.Delete(product);
            await _unitOfWork.SaveChangesAsync();
            var dto = _mapper.Map<ProductDto>(product);
            return Result<ProductDto>.Success(dto);
        }


        public async Task<Result<ProductDto?>> GetByIdAsync(int id)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(id);
            if (product == null)
            { 
                return Result<ProductDto?>.Failure("Product Not Found");
            }
            var dto = _mapper.Map<ProductDto?>(product);
            return Result<ProductDto?>.Success(dto);
        }

        public async Task<Result<UpdateProductDto>> UpdateAsync(UpdateProductDto updateProduct)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(updateProduct.Id);
            if (product == null)
            {
                return Result<UpdateProductDto>.Failure("Product Not Found");
            }
            _mapper.Map(updateProduct, product);
            _unitOfWork.Products.Update(product);
            await _unitOfWork.SaveChangesAsync();
            var dto = _mapper.Map<UpdateProductDto>(product);
            return Result<UpdateProductDto>.Success(dto);
        }

        public Task<PagedResult<ProductDto>> SearchAsync(ProductFilter filter)
        {
            return _genericService.SearchAsync<ProductDto>(filter);
        }
    }   
}
