using backend.Filters;
using backend.Interfaces;
using backend.Models;
using backend.Models.Cart;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/cart")]
    public class CartController : BaseController
    {
        private readonly ICartService _service;
        private readonly ILogger<CartController> _logger;

        public CartController(ICartService service, ILogger<CartController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        [RequireGuestId]
        public async Task<IActionResult> GetCart()
        {
            var guestId = HttpContext.Items["guest_token"] as string;

            try
            {
                var cart = await _service.GetOrCreateCartAsync(guestId!);

                return Ok(ApiResponse<CartDto>.SuccessResponse(cart, "Cart retrieved successfully."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse.ErrorResponse("An error occurred while retrieving the cart.", [ex.Message]));
            }
        }

        [HttpPost("items")]
        [RequireGuestId]

        public async Task<IActionResult> AddItemToCart([FromBody] AddCartItemRequest request)
        {
            var guestId = HttpContext.Items["guest_token"] as string;
            try
            {
                var result = await _service.AddCartItemAsync(guestId!, request);

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
        [RequireGuestId]
        public async Task<IActionResult> RemoveItemFromCart(long cartItemId)
        {
            var guestId = HttpContext.Items["guest_token"] as string;

            try
            {
                var result = await _service.RemoveCartItemAsync(guestId!, cartItemId);

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
    }
}