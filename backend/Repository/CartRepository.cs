using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Data;
using backend.Interfaces;
using backend.Models.Cart;
using Microsoft.EntityFrameworkCore;

namespace backend.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext _db;
        public CartRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<CartItem> AddCartItemToCartAsync(long cartId, CartItem cartItem)
        {
            var cart = await _db.Carts.FindAsync(cartId) ?? throw new Exception("Cart not found");

            var existingProduct = await _db.CartItems.FirstOrDefaultAsync(ci => ci.CartId == cartId && ci.ProductId == cartItem.ProductId);

            if (existingProduct != null)
            {
                existingProduct.Quantity = cartItem.Quantity;
                _db.CartItems.Update(existingProduct);
                await _db.SaveChangesAsync();
                return existingProduct;
            }

            cartItem.CartId = cartId;
            await _db.CartItems.AddAsync(cartItem);
            await _db.SaveChangesAsync();

            return cartItem;
        }

        public async Task<Cart> CreateCartAsync(Cart cart)
        {
            await _db.Carts.AddAsync(cart);
            await _db.SaveChangesAsync();
            return cart;
        }

        public async Task<Cart?> GetCartByGuestIdAsync(string guestId)
        {
            return await _db.Carts.Include(c => c.CartItems).ThenInclude(ci => ci.Product).FirstOrDefaultAsync(c => c.GuestId == guestId);
        }

        public async Task<bool> RemoveCartItemAsync(long cartItemId)
        {
            var cartItem = await _db.CartItems.FindAsync(cartItemId);

            if (cartItem == null)
            {
                return false;
            }

            _db.CartItems.Remove(cartItem);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}