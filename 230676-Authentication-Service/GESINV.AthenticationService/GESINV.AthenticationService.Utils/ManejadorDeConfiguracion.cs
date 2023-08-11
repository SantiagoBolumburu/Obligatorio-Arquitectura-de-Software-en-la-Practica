using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GESINV.AthenticationService.Utils
{
    public class ManejadorDeConfiguracion
    {
        private readonly IConfiguration _configuration;

        public ManejadorDeConfiguracion(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public int ObtenerTiempoSegundosHastaTimeoutDeSesion()
        {
            return _configuration.GetValue<int>("TimeOuts:SegundosHastaTimeout");
        }

        public int ObtenerTiempoSegundosHastaTimeoutDeApikey()
        {
            return _configuration.GetValue<int>("TimeOuts:SegundosHastaTimeoutApikey");
        }

        public int ObtenerDiasHastaVencimientoDeInvitacion()
        {
            return _configuration.GetValue<int>("TimeOuts:DiasHastaVencimientoDeInvitacion");
        }
    }
}
