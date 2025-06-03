using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Data;
using backend.Interfaces;
using backend.Models.Products;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductsController(IProductService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts([FromQuery] ProductPagedRequest request)
        {
            if (request.PageNumber.HasValue && request.PageSize.HasValue)
            {
                var pagedProducts = await _service.GetPagedProductsAsync(request);
                return Ok(pagedProducts);
            }
            
            var products = await _service.GetAllProductsAsync();

            return Ok(products);
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetProductById([FromRoute] long id)
        {
            var product = await _service.GetProductByIdAsync(id);

            if (product == null)
            {
                return NotFound("Product not found.");
            }
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest request)
        {
            var createdProduct = await _service.CreateProductAsync(request);
            return CreatedAtAction(nameof(GetProductById), new { id = createdProduct!.Id }, createdProduct);
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> UpdateProduct([FromRoute] long id, [FromBody] UpdateProductRequest request)
        {
            var updatedProduct = await _service.UpdateProductAsync(id, request);

            if (updatedProduct == null)
            {
                return NotFound("Product not found.");
            }

            return Ok(updatedProduct);
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] long id)
        {
            var deletedProduct = await _service.DeleteProductAsync(id);

            if (deletedProduct == false)
            {
                return NotFound("Product not found.");
            }

            return NoContent();
        }
    }
}