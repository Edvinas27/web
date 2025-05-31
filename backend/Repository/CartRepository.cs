using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Data;
using backend.Interfaces;
using backend.Models.Cart;
using Microsoft.EntityFrameworkCore;
using backend.Extensions.Mappers;


namespace backend.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly IProductRepository _prodRepo;
        private readonly ApplicationDbContext _db;

        public CartRepository(IProductRepository prodRepo, ApplicationDbContext db)
        {
            _prodRepo = prodRepo;
            _db = db;
        }
        public async Task AddCartItemToCartAsync(AddCartItemRequest cartItem, string guestId)
        {
            var product = await _prodRepo.GetProductByIdAsync(cartItem.ProductId);
            var cart = await GetCartByGuestIdAsync(guestId);

            if (product == null)
            {
                throw new ArgumentException("Product not found", nameof(cartItem.ProductId));
            }

            if (cart == null)
            {
                throw new ArgumentException("Cart not found for the given guest ID", nameof(guestId));
            }

            var existingCartItem = await _db.CartItems.FirstOrDefaultAsync(c => c.CartId == cart.Id && c.ProductId == cartItem.ProductId);

            if (existingCartItem != null)
            {
                existingCartItem.Quantity += cartItem.Quantity;
                _db.CartItems.Update(existingCartItem);
            }
            else
            {
                await _db.CartItems.AddAsync(cartItem.ToCartItem(cart.Id));
            }

            await _db.SaveChangesAsync();

        }

        public Task ClearCartAsync(string guestId)
        {
            throw new NotImplementedException();
        }

        public async Task<Cart> CreateCartAsync(string guestId)
        {
            var cart = new Cart
            {
                GuestId = guestId,
                CreatedAt = DateTime.UtcNow,
                CartItems = new List<CartItem>()
            };

            await _db.Carts.AddAsync(cart);
            await _db.SaveChangesAsync();

            return cart;
        }

        public async Task<CartDto?> GetCartByGuestIdAsync(string guestId)
        {
            var cart = await _db.Carts.FirstOrDefaultAsync(c => c.GuestId == guestId);

            if (cart == null)
            {
                return null;
            }

            // Ensure the cart items are loaded
            await _db.Entry(cart).Collection(c => c.CartItems).LoadAsync();

            return cart.ToCartDto();
        }

        public Task<Cart?> GetCartByIdAsync(long cartId)
        {
            throw new NotImplementedException();
        }

        public Task<CartItem?> GetCartItemByIdAsync(long cartItemId)
        {
            throw new NotImplementedException();
        }

        public Task RemoveCartItemAsync(long cartItemId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateCartItemAsync(CartItem cartItem)
        {
            throw new NotImplementedException();
        }
    }
}