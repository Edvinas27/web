using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Extensions.Mappers;
using backend.Interfaces;
using backend.Models.Cart;
using backend.Models.Products;


//CIA VISKAS DAR LABAI SKETCHY, REIKIA NORMALIAI SUTVARKYT SU REDIS

namespace backend.Services
{
    public class CartService : ICartService
    {
        private readonly IProductService _prodService;
        private readonly ICartRepository _cartRepo;
        private readonly ILogger<CartService> _logger;
        private readonly IRedisCacheService _cacheService;
        public CartService(IProductService prodService, ICartRepository cartRepo, ILogger<CartService> logger, IRedisCacheService cacheService)
        {
            _prodService = prodService;
            _cartRepo = cartRepo;
            _logger = logger;
            _cacheService = cacheService;
        }
        public async Task<AddCartItemResponse> AddCartItemAsync(string guestId, AddCartItemRequest request)
        {
            var product = await _prodService.GetProductByIdAsync(request.ProductId);

            if (product == null)
            {
                throw new ArgumentException("Product not found");
            }

            var cart = await _cacheService.GetDataAsync<Cart>($"cart_{guestId}");

            if (cart == null)
            {
                _logger.LogInformation("Cart not found in cache for guest ID: {GuestId}, fetching from repository", guestId);
                cart = await _cartRepo.GetCartByGuestIdAsync(guestId);
                if (cart == null)
                {
                    _logger.LogInformation("Cart not found in db for guest ID: {GuestId}, creating a new cart", guestId);
                    cart = await CreateCartInternalAsync(guestId);
                }
            }

            var cartItem = new CartItem
            {
                ProductId = request.ProductId,
                Quantity = request.Quantity,
            };

            var addedItem = await _cartRepo.AddCartItemAsync(cart.Id, cartItem);

            await _cacheService.SetDataAsync($"cart_{guestId}", cart);
            return addedItem.ToAddCartItemResponse();
        }



        public async Task<CartDto> GetOrCreateCartAsync(string guestId)
        {

            var cart = await _cartRepo.GetCartByGuestIdAsync(guestId);

            if (cart == null)
            {
                cart = await CreateCartInternalAsync(guestId);
                await _cacheService.SetDataAsync($"cart_{guestId}", cart);
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