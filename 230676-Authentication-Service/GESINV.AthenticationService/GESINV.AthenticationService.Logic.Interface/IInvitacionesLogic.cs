using GESINV.AthenticationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GESINV.AthenticationService.Logic.Interface
{
    public interface IInvitacionesLogic
    {
        InvitacionOutModel Crear(InvitacionModel invitacionModel);
        InvitacionOutModel? Obtener(Guid invitacionId);
    }
}
