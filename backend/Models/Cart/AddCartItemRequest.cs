using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models.Cart
{
    public class AddCartItemRequest
    {
        [Required(ErrorMessage = "Product ID is required.")]
        [Range(1, long.MaxValue, ErrorMessage = "Product ID must be a positive number.")]
        public long ProductId { get; set; }
        
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        [Required(ErrorMessage = "Quantity is required.")]
        public int Quantity { get; set; }
    }
}