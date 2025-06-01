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
        private readonly IJwtService _jwt;
        private readonly ICartService _service;

        public CartController(IJwtService jwt, ICartService service)
        {
            _jwt = jwt;
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetCart()
        {
            var guestId = GetGuestIdFromRequest();
            if (guestId == null)
            {
                return Unauthorized("Invalid or missing guest token.");
            }

            var cart = await _service.GetCartByGuestIdAsync(guestId);

            if (cart == null)
            {
                return NotFound("Cart not found for the guest.");
            }

            return Ok(cart);
        }

        [HttpGet("{cartId:long}")]
        public async Task<IActionResult> GetCartById([FromRoute] long cartId)
        {
            var cart = await _service.GetCartByIdAsync(cartId);

            if (cart == null)
            {
                return NotFound("Cart not found.");
            }

            return Ok(cart);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCart()
        {
            var guestId = GetGuestIdFromRequest();

            if (guestId == null)
            {
                return Unauthorized("Invalid or missing guest token.");
            }

            var cart = await _service.CreateCartAsync(guestId);

            if (cart == null)
            {
                return BadRequest("Failed to create cart.");
            }

            return CreatedAtAction(nameof(GetCartById), new { cartId = cart.Id }, cart);
        }

        [HttpPost("add-item")]
        public async Task<IActionResult> AddCartItem([FromBody] AddCartItemRequest request)
        {
            var guestId = GetGuestIdFromRequest();

            if (guestId == null)
            {
                return Unauthorized("Invalid or missing guest token.");
            }
            var cart = await _service.GetCartByGuestIdAsync(guestId);

            if (cart == null)
            {
                return NotFound("Cart not found for the guest.");
            }

            var cartItem = await _service.AddCartItemAsync(cart.Id, request);

            if (cartItem == null)
            {
                return BadRequest("Failed to add item to cart.");
            }

            return CreatedAtAction(nameof(GetCartById), new { cartId = cart.Id }, cartItem);
        }   


        private string? GetGuestIdFromRequest()
        {
            var token = Request.Cookies["guest_token"];

            if (string.IsNullOrEmpty(token))
            {
                return null;
            }

            return _jwt.GetGuestIdFromToken(token);
        }
    }
}