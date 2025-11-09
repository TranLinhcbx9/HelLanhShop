using HelLanhShop.Application.Common.Interfaces;
using HelLanhShop.Application.Products.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelLanhShop.Domain.Entities;
using HelLanhShop.Application.Products.DTOs;

namespace HelLanhShop.Application.Products.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<CreateProduct> CreateAsync(CreateProduct createProduct)
        {
            var product = _mapper.Map<Product>(createProduct);
            await _unitOfWork.Products.AddAsync(product);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<CreateProduct>(product);
        }

        public async Task<ProductDto> DeleteAsync(int id)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(id);
            if (product == null)
            {
                throw new Exception("Product not found");
            }
            _unitOfWork.Products.Delete(product);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<IEnumerable<ProductDto>> GetAllAsync()
        {
            var productlist = await _unitOfWork.Products.GetAllAsync();
            return _mapper.Map<IEnumerable<ProductDto>>(productlist);
        }

        public async Task<ProductDto?> GetByIdAsync(int id)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(id);
            if (product == null) return null;
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<UpdateProduct> UpdateAsync(UpdateProduct updateProduct)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(updateProduct.Id);
            if (product == null)
            {
                throw new Exception("Product not found");
            }
            _mapper.Map(updateProduct, product);
            _unitOfWork.Products.Update(product);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<UpdateProduct>(product);
        }
    }
}
