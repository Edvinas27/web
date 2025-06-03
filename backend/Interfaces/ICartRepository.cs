using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Models.Cart;

namespace backend.Interfaces
{
    public interface ICartRepository
    {
        
        Task<Cart> CreateCartAsync(Cart cart);
        Task<Cart?> GetCartByGuestIdAsync(string guestId);
        Task<CartItem> AddCartItemAsync(long cartId, CartItem cartItem);
        Task<bool> RemoveCartItemAsync(long cartItemId);
    }
}