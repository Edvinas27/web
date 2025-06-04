using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Interfaces;
using backend.Models;
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
            try
            {
                var guestId = GetGuestIdFromRequest();

                if (guestId == null)
                {
                    return Unauthorized(ApiResponse.ErrorResponse("Invalid or missing guest token."));
                }

                var cart = await _service.GetOrCreateCartAsync(guestId);

                return Ok(ApiResponse<CartDto>.SuccessResponse(cart, "Cart retrieved successfully."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse.ErrorResponse("An error occurred while retrieving the cart.", [ex.Message]));
            }
        }

        [HttpPost("items")]
        public async Task<IActionResult> AddItemToCart([FromBody] AddCartItemRequest request)
        {
            try
            {
                var guestId = GetGuestIdFromRequest();

                if (guestId == null)
                {
                    return Unauthorized(ApiResponse.ErrorResponse("Invalid or missing guest token."));
                }

                var result = await _service.AddCartItemAsync(guestId, request);

                return Ok(ApiResponse<AddCartItemResponse>.SuccessResponse(result, "Item added to cart successfully."));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ApiResponse.ErrorResponse("Invalid request.", [ex.Message]));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse.ErrorResponse("An error occurred while adding item to cart.", [ex.Message]));
            }
        }

        [HttpDelete("items/{cartItemId}")]
        public async Task<IActionResult> RemoveItemFromCart(long cartItemId)
        {
            try
            {
                var guestId = GetGuestIdFromRequest();

                if (guestId == null)
                {
                    return Unauthorized(ApiResponse.ErrorResponse("Invalid or missing guest token."));
                }

                var result = await _service.RemoveCartItemAsync(guestId, cartItemId);

                if (!result)
                {
                    return NotFound(ApiResponse.ErrorResponse("Cart item not found or could not be removed."));
                }

                return Ok(ApiResponse.SuccessResponse("Item removed from cart successfully."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse.ErrorResponse("An error occurred while removing item from cart.", [ex.Message]));
            }
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