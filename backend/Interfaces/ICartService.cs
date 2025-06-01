using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Models.Cart;

namespace backend.Interfaces
{
    public interface ICartService
    {
        Task<Cart> CreateCartAsync(string guestId);
        Task<CartDto?> GetCartByIdAsync(long cartId);
        Task<CartDto?> GetCartByGuestIdAsync(string guestId);
        Task<AddCartItemResponse> AddCartItemAsync(long cartId, AddCartItemRequest request);
        Task<UpdateCartItemResponse?> UpdateCartItemAsync(long cartItemId, UpdateCartItemRequest request);
        Task<bool> RemoveCartItemAsync(long cartItemId);
    }
}