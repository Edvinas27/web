using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models.Cart
{
    public class CartDto
    {
        public long Id { get; set; }
        public string GuestId { get; set; } = string.Empty;
        public List<CartItemResponse> CartItems { get; set; } = [];

        public DateTime CreatedAt { get; set; }
    }

    public class CartItemResponse
    {
        public long Id { get; set; }
        public long ProductId { get; set; }
        public int Quantity { get; set; }
    }
}