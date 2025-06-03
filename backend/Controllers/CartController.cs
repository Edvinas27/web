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
    [Route("api/cart")]
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

            var cart = await _service.GetOrCreateCartAsync(guestId);

            return Ok(cart);
        }

        [HttpPost("items")]
        public async Task<IActionResult> AddItemToCart([FromBody] AddCartItemRequest request)
        {
            var guestId = GetGuestIdFromRequest();

            if (guestId == null)
            {
                return Unauthorized("Invalid or missing guest token.");
            }

            var result = await _service.AddCartItemAsync(guestId, request);

            if (result == null)
            {
                return BadRequest("Failed to add item to cart.");
            }

            return Ok(result);
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