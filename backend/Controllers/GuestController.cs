using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using backend.Interfaces;
using backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/guest")]
    public class GuestController : ControllerBase
    {
        private readonly IJwtService _jwt;

        public GuestController(IJwtService jwt)
        {
            _jwt = jwt;
        }

        [HttpGet("token")]
        public IActionResult GetGuestToken()
        {
            try
            {
                if (!Request.Cookies.ContainsKey("guest_token"))
                {
                    var guestId = Guid.NewGuid().ToString();
                    var token = _jwt.GenerateToken(guestId);

                    Response.Cookies.Append("guest_token", token, new CookieOptions
                    {
                        // PREVENTS JS ACCESS, XSS protection
                        HttpOnly = true,
                        // Secure flag if true ensures the cookie is sent over HTTPS only, prod always true
                        Secure = false,
                        // I dont quite understand this yet :(
                        SameSite = SameSiteMode.Lax,
                        Expires = DateTimeOffset.UtcNow.AddHours(1)
                    });

                    return Ok(ApiResponse.SuccessResponse("Guest token generated successfully."));
                }

                return Ok(ApiResponse.SuccessResponse("Guest token already exists."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse.ErrorResponse("An error occurred while generating the guest token.", [ex.Message]));
            }
        }
    }
}