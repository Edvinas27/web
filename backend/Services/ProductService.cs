using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Extensions.Mappers;
using backend.Interfaces;
using backend.Models;
using backend.Models.Products;

namespace backend.Services
{
    public class ProductsService : IProductService
    {
    private readonly IProductRepository _prodRepo;
    private readonly ILogger<ProductsService> _logger;
    private readonly IRedisCacheService _cacheService;
        public ProductsService(IProductRepository prodRepo, ILogger<ProductsService> logger, IRedisCacheService cacheService)
        {
            _logger = logger;
            _prodRepo = prodRepo;
            _cacheService = cacheService;
        }


        public async Task<CreateProductResponse> CreateProductAsync(CreateProductRequest product)
        {
            _logger.LogInformation("Creating a new product with name: {ProductName}", product.Name);
            var newProduct = new Product
            {
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                Category = product.Category,
                Images = product.Images?.Select(dto => new Images
                {
                    Url = dto.Url,
                    Context = dto.Context
                }).ToList() ?? [],
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
            var cacheProducts = await _cacheService.GetDataAsync<IEnumerable<Product>>("all_products");

            if (cacheProducts is not null)
            {
                _logger.LogInformation("Products found in cache, returnning cached data");
                return cacheProducts.Select(data => data.ToResponseGet());
            }


            _logger.LogInformation("Products not found in cache, caching...");
            var products = await _prodRepo.GetAllProductsAsync();

            await _cacheService.SetDataAsync<IEnumerable<Product>>("all_products", products);

            return products.Select(data => data.ToResponseGet());
        }

        public async Task<IEnumerable<GetProductResponse>> GetPagedProductsAsync(ProductPagedRequest request)
        {
            var products = await _prodRepo.GetPagedProductsAsync(request);

            _logger.LogInformation("Retrieving paged products: Page {PageNumber}, Size {PageSize}", request.PageNumber, request.PageSize);
            return products.Select(data => data.ToResponseGet());
        }

        public async Task<GetProductResponse?> GetProductByIdAsync(long id)
        {
            _logger.LogInformation("Trying to retrieve product {id} from cache", id);
            var product = await _cacheService.GetDataAsync<Product>($"product_{id}");

            if (product == null)
            {
                _logger.LogWarning("Product with ID: {ProductId} not found in cache", id);
                product = await _prodRepo.GetProductByIdAsync(id);
                if (product == null)
                {
                    _logger.LogWarning("Product with ID: {ProductId} not found in database", id);
                    return null;
                }
                _logger.LogInformation("Product with ID: {ProductId} found in database, caching it", id);
                await _cacheService.SetDataAsync($"product_{id}", product);
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
                Category = request.Category,
                Images = request.Images?.Select(dto => new Images
                {
                    Url = dto.Url,
                    Context = dto.Context
                }).ToList() ?? [],
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