using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models.Products
{
    public class UpdateProductResponse
    {
        public long Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public string Description { get; set; } = string.Empty;

        public string Category { get; set; } = string.Empty;
        public List<ImagesDto> Images { get; set; } = [];
        public DateTime CreatedAt { get; set; }
    }
}