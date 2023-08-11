using GESINV.AthenticationService.Logic.Interface;
using GESINV.AthenticationService.Models;
using GESINV.AthenticationService.Utils;
using GESINV.IdentityHandler;
using GESINV.IdentityHandler.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace GESINV.AthenticationService.Filters
{
    public class AuthorizationAndSessionSetUpFilter : Attribute, IAuthorizationFilter
    {
        private readonly string[] _rolesValidos;

        public AuthorizationAndSessionSetUpFilter(string[] rolesValidos)
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
                if(authToken != null)
                    token = authToken;
                else if (appkey != null)
                    token = appkey;
                else
                    throw new SecurityTokenValidationException();

                
                string secretKey = Environment.GetEnvironmentVariable(EnvarionmentVariablesNames.GESINV_JWT_SECRET)
                    ?? throw new InvalidOperationException();

                if (!tokenHandler.ValidateToken(token, secretKey))
                    throw new SecurityTokenValidationException();

                
                SessionInfo sessionInfo = tokenHandler.DeserializeSessionInfo(token)
                    ?? throw new SecurityTokenValidationException();

                ISessionsLogic sessionsLogic = GetSessionsLogic(context);
                if (!sessionsLogic.ExisteSession(sessionInfo.SessionId))
                    throw new SecurityTokenValidationException();

                if (_rolesValidos.Length > 0 && !_rolesValidos.Contains(sessionInfo.Rol))
                    throw new UnauthorizedAccessException("No cuenta con el rol nesesario para realizar la operacion.");

                sessionsLogic.RenovarSession(sessionInfo.SessionId);
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

        private ISessionsLogic GetSessionsLogic(AuthorizationFilterContext context)
        {
            var sessionsLogicType = typeof(ISessionsLogic);
            object sessionsLogicObject = context.HttpContext.RequestServices.GetService(sessionsLogicType);
            ISessionsLogic sessionsLogic = sessionsLogicObject as ISessionsLogic;

            return sessionsLogic;
        }

        private ITokenHandler GetTokenHandler(AuthorizationFilterContext context)
        {
            var tokenHandlerType = typeof(ITokenHandler);
            object tokenHandlerObject = context.HttpContext.RequestServices.GetService(tokenHandlerType);
            ITokenHandler tokenHandler = tokenHandlerObject as ITokenHandler;

            return tokenHandler;
        }
    }
}
