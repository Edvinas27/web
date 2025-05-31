using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace backend.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(string guestId);
        ClaimsPrincipal? ValidateToken(string token);
        string? GetGuestIdFromToken(string token);

    }
}