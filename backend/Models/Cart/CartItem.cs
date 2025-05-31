using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Models.Products;

namespace backend.Models.Cart
{
    public class CartItem
    {
        public long Id { get; set; }
        public long CartId { get; set; }
        public long ProductId { get; set; }
        public int Quantity { get; set; } = 1;
        
        public Cart? Cart { get; set; }
        public Product? Product { get; set; }
    }
}