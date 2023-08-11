using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GESINV.AthenticationService.Utils
{
    public class JwtHandler
    {
        public static bool ValidateJwt(string token, string secretKey)
        {
            bool esValido = false;

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false
            };

            SecurityToken validatedToken;
            var handler = new JwtSecurityTokenHandler();

            try
            {
                var principal = handler.ValidateToken(token, validationParameters, out validatedToken);
                esValido = true;
            }
            catch (Exception ex)
            {
                esValido = false;
            }

            return esValido;
        }
    }
}
