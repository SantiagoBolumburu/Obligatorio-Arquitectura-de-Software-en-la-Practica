using GESINV.AthenticationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GESINV.AthenticationService.Logic.Interface
{
    public interface ISessionsLogic
    {
        string CrearSession(CredencialesModel credenciales);
        void TeminarSession();
        bool ExisteSession(Guid sessionId);
        void RenovarSession(Guid sessionId);
    }
}
