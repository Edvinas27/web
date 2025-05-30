using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Models.Products;

namespace backend.Extensions.Mappers
{
    public static class ProductMapper
    {
        public static GetProductResponse ToResponse(this Product product)
        {
            return new GetProductResponse
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                ImageUrl = product.ImageUrl
            };
        }
    }
}