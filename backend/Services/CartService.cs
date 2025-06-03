using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Extensions.Mappers;
using backend.Interfaces;
using backend.Models.Cart;
using backend.Models.Products;

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
        public async Task<AddCartItemResponse> AddCartItemAsync(string guestId, AddCartItemRequest request)
        {
            var product = await _prodRepo.GetProductByIdAsync(request.ProductId);

            if (product == null)
            {
                throw new ArgumentException("Product not found");
            }

            var cart = await GetCartInternalAsync(guestId);

            var cartItem = new CartItem
            {
                ProductId = request.ProductId,
                Quantity = request.Quantity,
            };

            var addedItem = await _cartRepo.AddCartItemAsync(cart.Id, cartItem);

            return addedItem.ToAddCartItemResponse();
        }


        public async Task<CartDto> GetOrCreateCartAsync(string guestId)
        {
            var cart = await _cartRepo.GetCartByGuestIdAsync(guestId);

            if (cart == null)
            {
                cart = await CreateCartInternalAsync(guestId);
            }

            return cart.ToCartDto();
        }

        public async Task<bool> RemoveCartItemAsync(string guestId, long cartItemId)
        {
            var cart = await _cartRepo.GetCartByGuestIdAsync(guestId);

            if (cart == null)
            {
                return false;
            }

            var cartItem = cart.CartItems?.FirstOrDefault(ci => ci.Id == cartItemId);

            if (cartItem == null)
            {
                return false;
            }

            return await _cartRepo.RemoveCartItemAsync(cartItemId);
        }

        private async Task<Cart> GetCartInternalAsync(string guestId)
        {
            var cart = await _cartRepo.GetCartByGuestIdAsync(guestId);
            if (cart == null)
            {
                cart = await CreateCartInternalAsync(guestId);
            }
            return cart;

        }

        private async Task<Cart> CreateCartInternalAsync(string guestId)
        {
            var cart = new Cart
            {
                GuestId = guestId,
                CreatedAt = DateTime.UtcNow,
            };

            return await _cartRepo.CreateCartAsync(cart);
        }
    }
}