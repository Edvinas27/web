using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Models.Products;

namespace backend.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<GetProductResponse>> GetAllProductsAsync();
        Task<GetProductResponse?> GetProductByIdAsync(long id);
        Task<CreateProductResponse> CreateProductAsync(CreateProductRequest product);
        Task<UpdateProductResponse?> UpdateProductAsync(long id, UpdateProductRequest request);
        Task<bool> DeleteProductAsync(long id);
    }
}