using GESINV.AthenticationService.Dominio;
using GESINV.AthenticationService.Filters;
using GESINV.AthenticationService.Logic.Interface;
using GESINV.AthenticationService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GESINV.AthenticationService.Controllers
{
    [ApiController]
    [Route("API/v2/invitaciones")]
    public class InvitacionesController : Controller
    {
        private IInvitacionesLogic _invitacionesLogic;

        public InvitacionesController(IInvitacionesLogic invitacionesLogic)
        {
            _invitacionesLogic = invitacionesLogic;
        }


        [HttpPost]
        [AuthorizationAndSessionSetUpFilter(new string[] { Roles.RolAdministrador })]
        public IActionResult CrearInvitacion([FromBody] InvitacionModel invitacion)
        {
            InvitacionOutModel invitacionCreada = _invitacionesLogic.Crear(invitacion);

            return Ok(invitacionCreada);
        }

        [HttpGet("{invitacionId}")]
        public IActionResult ObtenerInvitacion(Guid invitacionId)
        {
            InvitacionOutModel? invitacionCreada = _invitacionesLogic.Obtener(invitacionId);

            return Ok(invitacionCreada);
        }
    }
}
