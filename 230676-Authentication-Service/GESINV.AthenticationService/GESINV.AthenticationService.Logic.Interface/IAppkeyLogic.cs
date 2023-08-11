using GESINV.AthenticationService.Logic.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GESINV.AthenticationService.Logic.Interface
{
    public interface IAppkeyLogic
    {
        string CrearAppKey();
        string ObtenerAppkeyActual();
        void TeminarApiKey();
    }
}
