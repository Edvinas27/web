using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Interfaces;
using backend.Models.Cart;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ICartRepository _cartRepo;

        public CartController(ICartRepository cartRepo)
        {
            _cartRepo = cartRepo;
        }

        [HttpGet("{guestId}")]
        public async Task<IActionResult> GetCartByGuestId([FromRoute]string guestId)
        {
            var cart = await _cartRepo.GetCartByGuestIdAsync(guestId);
            if (cart == null)
            {
                return NotFound();
            }

            return Ok(cart);
        }

        [HttpPost("{guestId}")]
        public async Task<IActionResult> CreateCart([FromRoute]string guestId)
        {
            var cart = await _cartRepo.CreateCartAsync(guestId);
            return CreatedAtAction(nameof(GetCartByGuestId), new { guestId = cart.GuestId }, cart);
        }

        [HttpPost("{guestId}/items")]
        public async Task<IActionResult> AddCartItem([FromRoute]string guestId, [FromBody] AddCartItemRequest cartItem)
        {
            var cart = await _cartRepo.GetCartByGuestIdAsync(guestId);
            if (cart == null)
            {
                return NotFound();
            }
            await _cartRepo.AddCartItemToCartAsync(cartItem, guestId);
            return Ok(new { message = "Item added to cart successfully" });
        }
    }
}