using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Extensions.Mappers;
using backend.Interfaces;
using backend.Models.Products;

namespace backend.Services
{
    public class ProductsService : IProductService
    {
    private readonly IProductRepository _prodRepo;
    private readonly ILogger<ProductsService> _logger;
        public ProductsService(IProductRepository prodRepo, ILogger<ProductsService> logger)
        {
            _logger = logger;
            _prodRepo = prodRepo;
        }

        public async Task<CreateProductResponse> CreateProductAsync(CreateProductRequest product)
        {
            _logger.LogInformation("Creating a new product with name: {ProductName}", product.Name);
            var newProduct = new Product
            {
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                ImageUrl = product.ImageUrl,
                CreatedAt = DateTime.UtcNow
            };

            var createdProduct = await _prodRepo.CreateProductAsync(newProduct);
            return createdProduct.ToResponseCreate();
        }

        public async Task<bool> DeleteProductAsync(long id)
        {
            _logger.LogInformation("Deleting product with ID: {ProductId}", id);
            return await _prodRepo.DeleteProductAsync(id);
        }

        public async Task<IEnumerable<GetProductResponse>> GetAllProductsAsync()
        {
            _logger.LogInformation("Retrieving all products");
            var products = await _prodRepo.GetAllProductsAsync();

            return products.Select(data => data.ToResponseGet());
        }

        public async Task<GetProductResponse?> GetProductByIdAsync(long id)
        {
            _logger.LogInformation("Retrieving product with ID: {ProductId}", id);
            var product = await _prodRepo.GetProductByIdAsync(id);

            if (product == null)
            {
                _logger.LogWarning("Product with ID: {ProductId} not found", id);
                return null;
            }

            return product.ToResponseGet();

        }

        public async Task<UpdateProductResponse?> UpdateProductAsync(long id, UpdateProductRequest request)
        {
            _logger.LogInformation("Updating product with ID: {ProductId}", id);
            var product = new Product
            {
                Name = request.Name,
                Price = request.Price,
                Description = request.Description,
                ImageUrl = request.ImageUrl
            };

            var updatedProduct = await _prodRepo.UpdateProductAsync(id, product);

            if (updatedProduct == null)
            {
                _logger.LogWarning("Product with ID: {ProductId} not found for update", id);
                return null;
            }

            return updatedProduct.ToResponseUpdate();
        }
    }
}