using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;
using System.Linq;

namespace delete_comment_ms.Middleware
{
    public class JwtMiddleware
    {
        private static readonly string SecretKey = "ec4d98b7a5e9f1183b68debf34e73b9f0fa34b82c5eb7b3bdf4fc093824f83e8";

        public static Guid? ValidateJwtToken(string token)
        {
            var mySecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = mySecurityKey,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out var validatedToken);

                var userId = principal.FindFirstValue(ClaimTypes.NameIdentifier);
                return userId == null ? (Guid?)null : Guid.Parse(userId);
            }
            catch
            {
                return null;
            }
        }
    }
}
