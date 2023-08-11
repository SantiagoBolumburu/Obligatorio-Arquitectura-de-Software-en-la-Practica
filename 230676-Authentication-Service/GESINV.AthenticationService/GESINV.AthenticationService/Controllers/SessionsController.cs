using GESINV.AthenticationService.Dominio;
using GESINV.AthenticationService.Filters;
using GESINV.AthenticationService.Logic.Interface;
using GESINV.AthenticationService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GESINV.AthenticationService.Controllers
{
    [ApiController]
    [Route("API/v2/sessions")]
    public class SessionsController : Controller
    {
        private ISessionsLogic _sessionsLogic;

        public SessionsController(ISessionsLogic sessionsLogic)
        {
            _sessionsLogic = sessionsLogic;
        }

        [HttpPost]
        public IActionResult CrearSession([FromBody] CredencialesModel credenciales)
        {
            string tokenString = _sessionsLogic.CrearSession(credenciales);

            return Ok(new { token = tokenString });
        }

        [HttpDelete]
        [AuthorizationAndSessionSetUpFilter(new string[] { })]
        public IActionResult DeleteSession()
        {
            _sessionsLogic.TeminarSession();

            return Ok();
        }

        [HttpGet("health")]
        [AuthorizationAndSessionSetUpFilter(new string[] { })]
        public IActionResult SessionIsActiveAndValid()
        {
            //LAS VALIDACIONES LAS HACE EL FILTER
            /*
             * Este endpoint es para poder verificar si el token es valido y se corresponde a una 
             *  session que todabia existe.
             * Esas dos validaciones las hace el "AuthorizationAndSessionSetUpFilter", por lo que,
             *  llegar al Ok() (responder 200) en este endpoint se considera correcto.
             */

            return Ok();
        }
    }
}
