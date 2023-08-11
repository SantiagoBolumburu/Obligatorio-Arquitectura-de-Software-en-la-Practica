using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GESINV.ProductsService.Utils
{
    public class ManejadorDeConfiguracion
    {
        private readonly IConfiguration _configuration;

        public ManejadorDeConfiguracion(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool ObtenerValidarIdentityToken_OrDefault()
        {
            return _configuration.GetValue<bool>("ServiceConfig:ValidateIdentityToken");
        }

        public int ObtenerIntervaloRealizacionDeVentas()
        {
            return _configuration.GetValue<int>("ServiceConfig:TimeInMillisecondUntilNextRealizacionVentaprogramadas");
        }
    }
}
