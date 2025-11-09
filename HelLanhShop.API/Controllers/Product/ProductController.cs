using HelLanhShop.Application.Products.DTOs;
using HelLanhShop.Application.Products.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HelLanhShop.API.Controllers.Product
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
        public async Task<IActionResult> GetAll()
        {
            var data = await _productService.GetAllAsync();
            return Ok(data);
        }
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await _productService.GetByIdAsync(id);
            return data == null ? NotFound() : Ok(data);
        }
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CreateProduct createProduct)
        {
            await _productService.CreateAsync(createProduct);
            return Ok(createProduct);
        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] UpdateProduct updateProduct)
        {
            await _productService.UpdateAsync(updateProduct);
            return Ok(updateProduct);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _productService.DeleteAsync(id);
            return Ok();
        }
    }
}
