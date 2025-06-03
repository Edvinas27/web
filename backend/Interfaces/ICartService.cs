using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Models.Cart;

namespace backend.Interfaces
{
    public interface ICartService
    {
        
        Task<CartDto> GetOrCreateCartAsync(string guestId);
        Task<AddCartItemResponse> AddCartItemAsync(string guestId, AddCartItemRequest request);
        Task<bool> RemoveCartItemAsync(string guestId, long cartItemId);
    }
}