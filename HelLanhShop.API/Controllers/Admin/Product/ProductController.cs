using HelLanhShop.API.Common.ApiResponse;
using HelLanhShop.Application.Common.Models;
using HelLanhShop.Application.Products.DTOs;
using HelLanhShop.Application.Products.Filters;
using HelLanhShop.Application.Products.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HelLanhShop.API.Controllers.Admin.Product
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet("GetAll")]
        public async Task<ActionResult<List<ProductAdminDto>>> GetAll()
        {
            var result = await _productService.GetAllAsync();

            if (!result.IsSuccess)
            {
                return StatusCode(500, ApiResponse.Fail(result.Error!));
            }
            return Ok(ApiResponse<List<ProductAdminDto>>.Ok(result.Data!));
        }
        [HttpGet("GetAllPaging")]
        public async Task<ActionResult<PagedResult<ProductAdminDto>>> GetAllPagingAsync(int pageIndex = 1, int pageSize = 10)
        {
            var result = await _productService.GetAllPagingAsync(pageIndex, pageSize);
            if (!result.IsSuccess)
            {
                return StatusCode(500, ApiResponse.Fail(result.Error!));
            }
            return Ok(ApiResponse<PagedResult<ProductAdminDto>>.Ok(result.Data!));
        }
        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<ProductAdminDto>> GetById(int id)
        {
            var result = await _productService.GetByIdAsync(id);
            if (!result.IsSuccess)
            {
                return NotFound(ApiResponse.Fail(result.Error!));
            }
            return Ok(ApiResponse<ProductAdminDto?>.Ok(result.Data!));
        }
        [HttpPost("Create")]
        public async Task<ActionResult<CreateProductDto>> Create([FromBody] CreateProductDto createProduct)
        {
            var result = await _productService.CreateAsync(createProduct);
            if (!result.IsSuccess)
            {
                return StatusCode(500, ApiResponse<CreateProductDto>.Fail(result.Error!));
            }
            return Ok(ApiResponse<CreateProductDto>.Ok(result.Data!));
        }
        [HttpPut("Update")]
        public async Task<ActionResult<UpdateProductDto>> Update([FromBody] UpdateProductDto updateProduct)
        {
            var result = await _productService.UpdateAsync(updateProduct);
            if (!result.IsSuccess)
            {
                return NotFound(ApiResponse.Fail(result.Error!));
            }
            return Ok(ApiResponse<UpdateProductDto>.Ok(result.Data!));
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<ProductAdminDto>> Delete(int id)
        {
            var result = await _productService.DeleteAsync(id);
            if (!result.IsSuccess)
            {
                return NotFound(ApiResponse.Fail(result.Error!));
            }
            return Ok(ApiResponse<ProductAdminDto>.Ok(result.Data!));
        }
        [HttpPost("Search")]
        public async Task<ActionResult<PagedResult<ProductAdminDto>>> Search([FromBody] ProductFilter filter)
        {
            var result = await _productService.SearchAsync(filter);
            
            return Ok(ApiResponse<PagedResult<ProductAdminDto>>.Ok(result));
        }
    }
}
