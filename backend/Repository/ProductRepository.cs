using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Data;
using backend.Extensions.Mappers;
using backend.Interfaces;
using backend.Models.Products;
using Microsoft.EntityFrameworkCore;

namespace backend.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _db;

        public ProductRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<GetProductResponse> CreateProductAsync(CreateProductRequest request)
        {
                var product = new Product
                {
                    Name = request.Name,
                    Price = request.Price,
                    Description = request.Description,
                    ImageUrl = request.ImageUrl,
                    CreatedAt = DateTime.UtcNow,
                };

                await _db.Products.AddAsync(product);
                await _db.SaveChangesAsync();

                return product.ToResponse();
        }

        public async Task<Product?> DeleteProductAsync(long id)
        {
            var response = await _db.Products.FindAsync(id);

            if (response == null)
            {
                return null;
            }

            _db.Products.Remove(response);
            await _db.SaveChangesAsync();

            return response;
        }

        public async Task<IEnumerable<GetProductResponse>> GetAllProductsAsync()
        {
            var response = await _db.Products.Select(data => data.ToResponse()).ToListAsync();

            return response;
        }

        public async Task<GetProductResponse?> GetProductByIdAsync(long id)
        {
            var response = await _db.Products.FindAsync(id);

            return response?.ToResponse();
        }

        public async Task<GetProductResponse?> UpdateProductAsync(long id, UpdateProductRequest request)
        {  
            var response = await _db.Products.FindAsync(id);

            if (response == null)
            {
                return null;
            }

            _db.Entry(response).CurrentValues.SetValues(request);
            await _db.SaveChangesAsync();

            return response.ToResponse();
        }
    }
}