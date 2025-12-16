using HelLanhShop.API.Common;
using HelLanhShop.API.Common.ApiResponses;
using HelLanhShop.Application.Common.Models;
using HelLanhShop.Application.Products.DTOs;
using HelLanhShop.Application.Products.Filters;
using HelLanhShop.Application.Products.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HelLanhShop.API.Controllers.Admin.Product
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductController : BaseController
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet("GetAll")]
        public async Task<ActionResult<ApiResponse<List<ProductAdminDto>>>> GetAll()
        {
            var result = await _productService.GetAllAsync();

            return FromResult(result);
        }
        [HttpPost("GetAllPaging")]
        public async Task<ActionResult<PagedResponse<ProductAdminDto>>> GetAllPagingAsync(RequestPagingProduct request)
        {
            var result = await _productService.GetAllPagingAsync(request);
            return FromPaged(result);
        }
        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<ApiResponse<ProductAdminDto?>>> GetById(int id)
        {
            var result = await _productService.GetByIdAsync(id);
            return FromResult(result);
        }
        [HttpPost("Create")]
        public async Task<ActionResult<ApiResponse<CreateProductDto>>> Create([FromBody] CreateProductDto createProduct)
        {
            var result = await _productService.CreateAsync(createProduct);
            return FromResult(result);
        }
        [HttpPut("Update")]
        public async Task<ActionResult<ApiResponse<UpdateProductDto>>> Update([FromBody] UpdateProductDto updateProduct)
        {
            var result = await _productService.UpdateAsync(updateProduct);
            return FromResult(result);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<ApiResponse<ProductAdminDto>>> Delete(int id)
        {
            var result = await _productService.DeleteAsync(id);
            return FromResult(result);
        }
        [HttpPost("Search")]
        public async Task<ActionResult<PagedResponse<ProductAdminDto>>> Search([FromBody] ProductFilter filter)
        {
            var result = await _productService.SearchAsync(filter);
            
            return FromPaged(result);
        }
    }
}
