using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Models.Products;

namespace backend.Models.Cart
{
    public class CartDto
    {
        public string GuestId { get; set; } = string.Empty;
        public List<CartItemResponse> CartItems { get; set; } = [];

        public DateTime CreatedAt { get; set; }

        public decimal TotalPrice => CartItems.Sum(item => item.Quantity * (item.Product?.Price) ?? 0);
    }

    public class CartItemResponse
    {
        public int Quantity { get; set; }
        public Product? Product { get; set; }
    }
}