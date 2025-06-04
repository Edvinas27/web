using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using backend.Models.Products;

namespace backend.Models
{
    public class Images
    {
        [Key]
        public long Id { get; set; }

        public long ProductId { get; set; }
        public string Context { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;

        public Product? Product { get; set; }
    }
}