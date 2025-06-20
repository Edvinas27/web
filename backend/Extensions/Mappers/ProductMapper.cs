using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Models.Products;

namespace backend.Extensions.Mappers
{
    public static class ProductMapper
    {
        public static GetProductResponse ToResponseGet(this Product product)
        {
            return new GetProductResponse
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                Category = product.Category,
                Images = product.Images.ToDtoList()
            };
        }

        public static CreateProductResponse ToResponseCreate(this Product product)
        {
            return new CreateProductResponse
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                Category = product.Category,
                Images = product.Images.ToDtoList(),
                CreatedAt = DateTime.UtcNow
            };
        }

        public static UpdateProductResponse ToResponseUpdate(this Product product)
        {
            return new UpdateProductResponse
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                Category = product.Category,
                Images = product.Images.ToDtoList(),
                CreatedAt = product.CreatedAt
            };
        }
    }
}