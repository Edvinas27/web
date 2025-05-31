using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models.Cart
{
    public class AddCartItemRequest
    {
        public long ProductId { get; set; }
        public int Quantity { get; set; } = 1;
    }
}