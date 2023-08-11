using GESINV.IdentityHandler.Abstractions;
using GESINV.IdentityHandler;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using GESINV.ProductsService.Utils;

namespace GESINV.ProductsService.Filters
{
    public class AuthorizationAndIdentitySetupFilter : Attribute, IAuthorizationFilter
    {
        private readonly string[] _rolesValidos;

        public AuthorizationAndIdentitySetupFilter(string[] rolesValidos)
        {
            _rolesValidos = rolesValidos;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            try
            {
                ITokenHandler tokenHandler = GetTokenHandler(context);

                string? appkey = tokenHandler.GetCurrentRequestToken(UsedHttpHeaders.API_KEY, TokenFormat.PLAIN);
                string? authToken = tokenHandler.GetCurrentRequestToken(UsedHttpHeaders.AUTHORIZATION, TokenFormat.BEARER);

                string token;
                if (authToken != null)
                    token = authToken;
                else if (appkey != null)
                    token = appkey;
                else
                    throw new SecurityTokenValidationException();

                bool validarToken = GetManejadorDeConfiguracion(context).ObtenerValidarIdentityToken_OrDefault();
                if (validarToken)
                {
                    string secretKey = Environment.GetEnvironmentVariable(EnvarionmentVariablesNames.GESINV_JWT_SECRET)
                        ?? throw new InvalidOperationException();

                    if (!tokenHandler.ValidateToken(token, secretKey))
                        throw new SecurityTokenValidationException();
                }

                SessionInfo sessionInfo = tokenHandler.DeserializeSessionInfo(token)
                    ?? throw new SecurityTokenValidationException();

                if (_rolesValidos.Length > 0 && !_rolesValidos.Contains(sessionInfo.Rol))
                    throw new UnauthorizedAccessException("No cuenta con el rol nesesario para realizar la operacion.");

                tokenHandler.SetCurrentSessionInfo(sessionInfo);
            }
            catch (SecurityTokenExpiredException ex)
            {
                context.Result = new ContentResult
                {
                    StatusCode = 401,
                    ContentType = "application/json",
                    Content = "Token invalido/expirado",
                };
            }
            catch (SecurityTokenValidationException ex)
            {
                context.Result = new ContentResult
                {
                    StatusCode = 401,
                    ContentType = "application/json",
                    Content = "Token ausente/no valido",
                };
            }
            catch (UnauthorizedAccessException ex)
            {
                context.Result = new ContentResult
                {
                    StatusCode = 403,
                    ContentType = "application/json",
                    Content = ex.Message,
                };
            }
            catch (ArgumentException ex)
            {
                context.Result = new ContentResult
                {
                    StatusCode = 401,
                    ContentType = "application/json",
                    Content = "Token no valido",
                };
            }
            catch (Exception ex)
            {
                context.Result = new ContentResult
                {
                    StatusCode = 500,
                    ContentType = "application/json",
                    Content = "Problema no identificado en el servidor",
                };
            }
        }

        private ITokenHandler GetTokenHandler(AuthorizationFilterContext context)
        {
            var tokenHandlerType = typeof(ITokenHandler);
            object tokenHandlerObject = context.HttpContext.RequestServices.GetService(tokenHandlerType);
            ITokenHandler tokenHandler = tokenHandlerObject as ITokenHandler;

            return tokenHandler;
        }

        private ManejadorDeConfiguracion GetManejadorDeConfiguracion(AuthorizationFilterContext context)
        {
            var type = typeof(ManejadorDeConfiguracion);
            object theObject = context.HttpContext.RequestServices.GetService(type);
            ManejadorDeConfiguracion manejadorDeConfiguracion = theObject as ManejadorDeConfiguracion;

            return manejadorDeConfiguracion;
        }
    }
}


