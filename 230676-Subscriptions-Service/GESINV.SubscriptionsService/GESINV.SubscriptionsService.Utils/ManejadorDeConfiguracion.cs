using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GESINV.SubscriptionsService.Utils
{
    public class ManejadorDeConfiguracion
    {
        private readonly IConfiguration _configuration;

        public ManejadorDeConfiguracion(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool ObtenerValidarIdentityToken()
        {
            return _configuration.GetValue<bool>("ServiceConfig:ValidateIdentityToken");
        }

        public int ObtenerCotaParaCorreoBajoStock()
        {
            return _configuration.GetValue<int>("ServiceConfig:CotaParaCorreoBajoStock");
        }

        public string? ObtenerDefaultFromEmail()
        {
            return _configuration.GetValue<string>("ServiceConfig:DefaultFromEmail");
        }

        public string? ObtenerDefaultVentaSubject()
        {
            return _configuration.GetValue<string>("ServiceConfig:DefaultVentaSubject");
        }

        public string? ObtenerDefaultCompraSubject()
        {
            return _configuration.GetValue<string>("ServiceConfig:DefaultCompraSubject");
        }

        public string? ObtenerDefaultStockSubject()
        {
            return _configuration.GetValue<string>("ServiceConfig:DefaultStockSubject");
        }
    }
}