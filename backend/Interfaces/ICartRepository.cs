using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Models.Cart;

namespace backend.Interfaces
{
    public interface ICartRepository
    {
        Task<CartDto?> GetCartByGuestIdAsync(string guestId);
        Task<Cart?> GetCartByIdAsync(long cartId);
        Task<CartItem?> GetCartItemByIdAsync(long cartItemId);
        Task AddCartItemToCartAsync(AddCartItemRequest cartItem, string guestId);
        Task UpdateCartItemAsync(CartItem cartItem);
        Task RemoveCartItemAsync(long cartItemId);
        Task ClearCartAsync(string guestId);
        Task<Cart> CreateCartAsync(string guestId);
    }
}