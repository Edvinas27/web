using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Models.Products;

namespace backend.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetPagedProductsAsync(ProductPagedRequest request);
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product?> GetProductByIdAsync(long id);
        Task<Product> CreateProductAsync(Product product);
        Task<Product?> UpdateProductAsync(long id, Product request);
        Task<bool> DeleteProductAsync(long id);
    }
}