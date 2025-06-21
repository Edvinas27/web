using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace backend.Filters
{
    public class RequireGuestId : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var token = context.HttpContext.Request.Cookies["guest_token"];

            if (string.IsNullOrEmpty(token))
            {
                var logger = context.HttpContext.RequestServices.GetService<ILogger<RequireGuestId>>();
                logger?.LogWarning("Guest token is null or missing in the request.");

                context.Result = new UnauthorizedObjectResult(
                    ApiResponse.ErrorResponse("Invalid or missing guest token.")
                );
                return;
            }

            
            context.HttpContext.Items["guest_token"] = token;
        }
    }
}