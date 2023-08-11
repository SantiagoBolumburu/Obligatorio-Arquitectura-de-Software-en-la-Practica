using GESINV.IdentityHandler.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;


namespace GESINV.IdentityHandler
{
    public class JwtTokenHandler : ITokenHandler
    {
        private SessionInfo? SESSION_INFO { get; set; }
        private readonly IHttpContextAccessor _httpContextAccessor;

        public JwtTokenHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public SessionInfo GetCurrentSessionInfo()
        {
            if (SESSION_INFO is null)
                throw new InvalidOperationException();
            return SESSION_INFO;
        }

        public void SetCurrentSessionInfo(SessionInfo sessionInfo)
        {
            SESSION_INFO = sessionInfo;
        }

        public string? GetCurrentRequestToken(string header, TokenFormat format)
        {
            HttpRequest request = _httpContextAccessor.HttpContext.Request;
            IHeaderDictionary headers = request.Headers;

            StringValues identityHeader = headers[header];
            if (!identityHeader.Any())
                return null;

            string token;
            if (format == TokenFormat.BEARER)
                token = identityHeader.ToString().Replace("Bearer ", "");
            else
                token = identityHeader.ToString();

            if (string.IsNullOrEmpty(token))
                return null;

            return token;
        }

        public SessionInfo? DeserializeSessionInfo(string token)
        {
            JwtSecurityTokenHandler jwtHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken jwt = jwtHandler.ReadJwtToken(token);
            SessionInfo? info = JsonSerializer.Deserialize<SessionInfo>(jwt.Payload.SerializeToJson());

            if (info is null)
                return null;

            return info;
        }

        public string GetSessionToken(SessionInfo sessionInfo, string secretKey)
        {
            Claim[] claims = new[]
{
                new Claim(SessionInfo.PROPERTY_NOMBRE , sessionInfo.Nombre),
                new Claim(SessionInfo.PROPERTY_EMAIL , sessionInfo.Email),
                new Claim(SessionInfo.PROPERTY_ROL, sessionInfo.Rol),
                new Claim(SessionInfo.PROPERTY_EMPRESANOMBRE, sessionInfo.Empresa),
                new Claim(SessionInfo.PROPERTY_SESSIONID , sessionInfo.SessionId.ToString()),
                new Claim(SessionInfo.PROPERTY_USUARIOID , sessionInfo.UsuarioId.ToString()),
                new Claim(SessionInfo.PROPERTY_EMPRESAID, sessionInfo.EmpresaId.ToString()),
            };


            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credenciales = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var securityToken = new JwtSecurityToken(
                        claims: claims,
                        signingCredentials: credenciales);

            string token = new JwtSecurityTokenHandler().WriteToken(securityToken);

            return token;
        }

        public bool ValidateToken(string token, string secretKey)
        {
            bool esValido;

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
