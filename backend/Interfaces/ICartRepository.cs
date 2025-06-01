using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Models.Cart;

namespace backend.Interfaces
{
    public interface ICartRepository
    {
        Task<CartItem?> GetCartItemByIdAsync(long cartItemId);
        Task<Cart> CreateCartAsync(Cart cart);
        Task<Cart?> GetCartByIdAsync(long cartId);
        Task<Cart?> GetCartByGuestIdAsync(string guestId);
        Task<IEnumerable<CartItem>> GetCartItemsByCartIdAsync(long cartId);
        Task<CartItem> AddCartItemAsync(long cartId, CartItem cartItem);
        Task<CartItem?> UpdateCartItemAsync(long cartItemId);
        Task<bool> RemoveCartItemAsync(long cartItemId);
    }
}