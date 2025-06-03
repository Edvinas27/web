using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Data;
using backend.Interfaces;
using backend.Models;
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
                return Ok(ApiResponse<IEnumerable<GetProductResponse>>.SuccessResponse(
                    pagedProducts,
                    "Paged products retrieved successfully."
                ));
            }

            var products = await _service.GetAllProductsAsync();


            return Ok(ApiResponse<IEnumerable<GetProductResponse>>.SuccessResponse(
                products,
                "Products retrieved successfully."
            ));
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetProductById([FromRoute] long id)
        {
            var product = await _service.GetProductByIdAsync(id);

            if (product == null)
            {
                return NotFound(ApiResponse<GetProductResponse>.ErrorResponse(
                    "Product not found.",
                    [$"Product with ID {id} does not exist."]
                ));
            }
            return Ok(ApiResponse<GetProductResponse>.SuccessResponse(
                product,
                "Product retrieved successfully."
            ));
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest request)
        {
            var createdProduct = await _service.CreateProductAsync(request);
            return CreatedAtAction(nameof(GetProductById), new { id = createdProduct!.Id }, ApiResponse<CreateProductResponse>.SuccessResponse(
                createdProduct,
                "Product created successfully."
            ));
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> UpdateProduct([FromRoute] long id, [FromBody] UpdateProductRequest request)
        {
            var updatedProduct = await _service.UpdateProductAsync(id, request);

            if (updatedProduct == null)
            {
                return NotFound(ApiResponse<UpdateProductResponse>.ErrorResponse(
                    "Product could not be updated.",
                    [$"Product with ID {id} does not exist."]
                ));
            }

            return Ok(ApiResponse<UpdateProductResponse>.SuccessResponse(
                updatedProduct,
                "Product updated successfully."
            ));
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] long id)
        {
            var deletedProduct = await _service.DeleteProductAsync(id);

            if (deletedProduct == false)
            {
                return NotFound(ApiResponse.ErrorResponse(
                    "Product could not be deleted.",
                    [$"Product with ID {id} does not exist."]
                ));
            }

            return NoContent();
        }
    }
}