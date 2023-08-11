using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace GESINV.AthenticationService.Filters
{
    public class ExceptionHandlingFilter : IExceptionFilter
    {
        public ExceptionHandlingFilter() { }

        public void OnException(ExceptionContext context)
        {
            try
            {
                throw context.Exception;
            }
            catch (DbUpdateException ex)
            {
                context.Result = new ContentResult
                {
                    StatusCode = 400,
                    ContentType = "application/json",
                    Content = "Violacion de Restricciones: se ingreso en un campo que devia ser UNICO (para la entidad) un valor ya presente en el sistema.",
                };
            }
            catch (ArgumentNullException ex)
            {
                context.Result = new ContentResult
                {
                    StatusCode = 400,
                    ContentType = "application/json",
                    Content = ex.Message,
                };
            }
            catch (ArgumentException ex)
            {
                context.Result = new ContentResult
                {
                    StatusCode = 400,
                    ContentType = "application/json",
                    Content = ex.Message,
                };
            }
            catch (InvalidOperationException ex)
            {
                context.Result = new ContentResult
                {
                    StatusCode = 401,
                    ContentType = "application/json",
                    Content = ex.Message,
                };
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
    }
}
