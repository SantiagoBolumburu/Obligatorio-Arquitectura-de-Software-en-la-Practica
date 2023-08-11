using GESINV.AthenticationService.Logic.Interface;
using GESINV.AthenticationService.Models;
using Microsoft.AspNetCore.Mvc;

namespace GESINV.AthenticationService.Controllers
{
    [ApiController]
    [Route("API/v2/usuarios")]
    public class UsuariosController : Controller
    {
        private IUsuariosLogic _usuarioLogic;

        public UsuariosController(IUsuariosLogic usuariosLogic)
        {
            _usuarioLogic = usuariosLogic;
        }

        [HttpPost]
        public IActionResult CrearUsuario([FromBody] UsuarioModel usuarioModel, [FromQuery] Guid? invitacionid)
        {
            UsuarioModel usuario;

            if (invitacionid is null)
            {
                usuario = _usuarioLogic.CreateAdminAndEmpresa(usuarioModel);
            }
            else
            {
                usuario = _usuarioLogic.CreateByInvitacion(usuarioModel, invitacionid.Value);
            }

            return Ok(usuario);
        }
    }
}
