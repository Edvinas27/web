using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models.Cart
{
    public class AddCartItemResponse
    {
        public long ProductId { get; set; }
        public int Quantity { get; set; }
    }
}