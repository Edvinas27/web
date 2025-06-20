using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using backend.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace backend.Services
{
    public class JwtService : IJwtService
    {
        private readonly string _secret;
        private readonly string _issuer;
        public JwtService(IConfiguration configuration)
        {
            _secret = configuration["JwtConfig:SecretKey"] ?? throw new ArgumentNullException("Jwt:SecretKey is not configured");
            _issuer = configuration["JwtConfig:Issuer"] ?? throw new ArgumentNullException("Jwt:Issuer is not configured");
        }

        public string GenerateToken(string guestId)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secret));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("guestId", guestId)
            };

            var token = new JwtSecurityToken
            (
                issuer: _issuer,
                audience: null,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(24),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public ClaimsPrincipal? ValidateToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_secret);

            try
            {
                return handler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = _issuer,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);
            }
            catch
            {

                return null;
            }
        }

    }
}