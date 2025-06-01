using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Extensions.Mappers;
using backend.Interfaces;
using backend.Models.Cart;

namespace backend.Services
{
    public class CartService : ICartService
    {
        private readonly IProductRepository _prodRepo;
        private readonly ICartRepository _cartRepo;
        public CartService(IProductRepository prodRepo, ICartRepository cartRepo)
        {
            _prodRepo = prodRepo;
            _cartRepo = cartRepo;
        }
        public async Task<AddCartItemResponse> AddCartItemAsync(long cartId, AddCartItemRequest request)
        {
            var product = await _prodRepo.GetProductByIdAsync(request.ProductId);

            if (product == null)
            {
                throw new Exception("Product not found");
            }

            var cartItem = new CartItem
            {
                ProductId = request.ProductId,
                Quantity = request.Quantity,
            };

            var addedItem = await _cartRepo.AddCartItemAsync(cartId, cartItem);

            return addedItem.ToAddCartItemResponse();

        }

        public async Task<Cart> CreateCartAsync(string guestId)
        {
            var cart = new Cart
            {
                GuestId = guestId,
                CreatedAt = DateTime.UtcNow,
            };

            return await _cartRepo.CreateCartAsync(cart);
        }

        public async Task<CartDto?> GetCartByGuestIdAsync(string guestId)
        {
            var cart = await _cartRepo.GetCartByGuestIdAsync(guestId);

            if (cart == null)
            {
                return null;
            }
            
            return cart.ToCartDto();
        }

        public async Task<CartDto?> GetCartByIdAsync(long cartId)
        {
            var cart = await _cartRepo.GetCartByIdAsync(cartId);
            if (cart == null)
            {
                return null;
            }

            return cart.ToCartDto();
        }

        public async Task<bool> RemoveCartItemAsync(long cartItemId)
        {
            return await _cartRepo.RemoveCartItemAsync(cartItemId);
        }

        public async Task<UpdateCartItemResponse?> UpdateCartItemAsync(long cartItemId, UpdateCartItemRequest request)
        {
            var cartItem = await _cartRepo.GetCartItemByIdAsync(cartItemId) ?? throw new Exception("Cart item not found");

            cartItem.Quantity = request.Quantity;
            var updatedItem = await _cartRepo.UpdateCartItemAsync(cartItemId);

            if (updatedItem == null)
            {
                return null;
            }

            return updatedItem.ToUpdateCartItemResponse();
        }
    }
}