using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using backend.Models.Products;

namespace backend.Models.Cart
{
    public class Cart
    {
        [Key]
        public long Id { get; set; }
        [Required(ErrorMessage = "Guest ID is required.")]
        public string GuestId { get; set; } = string.Empty;
        [Required(ErrorMessage = "Products are required.")]
        public List<CartItem> CartItems { get; set; } = [];

        public DateTime CreatedAt { get; set; }
    }
}