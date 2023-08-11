using GESINV.AthenticationService.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GESINV.AthenticationService.PersistanceAccess.Interface
{
    public interface IInvitacionesRepository
    {
        Invitacion? GetById(Guid id);
        Invitacion Update(Invitacion invitacion);
        Invitacion Create(Invitacion invitacion);
    }
}
