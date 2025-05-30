using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Models.Products;

namespace backend.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<GetProductResponse>> GetAllProductsAsync();
        Task<GetProductResponse?> GetProductByIdAsync(long id);
        Task<GetProductResponse> CreateProductAsync(CreateProductRequest request);
        Task<GetProductResponse?> UpdateProductAsync(long id, UpdateProductRequest request);
        Task<Product?> DeleteProductAsync(long id);
    }
}