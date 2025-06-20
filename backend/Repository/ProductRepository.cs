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

        public async Task<Product> CreateProductAsync(Product product)
        {
            await _db.Products.AddAsync(product);
            await _db.SaveChangesAsync();

            return product;
        }

        public async Task<bool> DeleteProductAsync(long id)
        {
            var product = await _db.Products.FindAsync(id);

            if (product == null)
            {
                return false;
            }

            _db.Products.Remove(product);
            await _db.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _db.Products.Include(p => p.Images).
                AsSplitQuery().
                AsNoTracking()
                .ToListAsync();
        }

        public async Task<Product?> GetProductByIdAsync(long id)
        {
            return await _db.Products.Include(p => p.Images)
            .AsSplitQuery()
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Product>> GetPagedProductsAsync(ProductPagedRequest request)
        {
            var pageNumber = request.PageNumber ?? 1;
            var pageSize = request.PageSize ?? 20;

            return await _db.Products
            .AsSplitQuery()
            .Include(p => p.Images)
            .OrderBy(p => p.Id)
            .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Product?> UpdateProductAsync(long id, Product request)
        {  
            var product = await _db.Products.FindAsync(id);

            if (product == null)
            {
                return null;
            }

            _db.Entry(product).CurrentValues.SetValues(request);
            await _db.SaveChangesAsync();

            return product;
        }
    }
}