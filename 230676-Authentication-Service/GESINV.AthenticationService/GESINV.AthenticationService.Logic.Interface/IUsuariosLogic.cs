using GESINV.AthenticationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GESINV.AthenticationService.Logic.Interface
{
    public interface IUsuariosLogic
    {
        UsuarioModel CreateAdminAndEmpresa(UsuarioModel usuario);
        UsuarioModel CreateByInvitacion(UsuarioModel usuarioModel, Guid invitacionid);
    }
}
