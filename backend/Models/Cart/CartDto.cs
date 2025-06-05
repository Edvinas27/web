using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Models.Products;

namespace backend.Models.Cart
{
    public class CartDto
    {
        public List<CartItemResponse> CartItems { get; set; } = [];
        public decimal TotalPrice => CartItems.Sum(item => item.Quantity * (item.Product?.Price) ?? 0);
    }

    public class CartItemResponse
    {
        public int Quantity { get; set; }
        public GetProductResponse? Product { get; set; }
    }
}