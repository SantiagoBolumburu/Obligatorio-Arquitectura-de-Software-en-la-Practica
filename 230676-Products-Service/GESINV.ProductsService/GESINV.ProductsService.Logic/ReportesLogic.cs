using GESINV.IdentityHandler.Abstractions;
using GESINV.ProductsService.BackingServices.Interface;
using GESINV.ProductsService.Dominio;
using GESINV.ProductsService.Logic.Interface;
using GESINV.ProductsService.Logic.Utils;
using GESINV.ProductsService.Models;
using GESINV.ProductsService.PersistanceAccess.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GESINV.ProductsService.Logic
{
    public class ReportesLogic : IReportesLogic
    {
        private readonly IEmailHandler _emailHandler;
        private readonly ITokenHandler _tokenHandler;
        private readonly IVentasRepository _ventasRepository;
        private readonly IComprasRepository _comprasRepository;

        public ReportesLogic(ITokenHandler tokenHandler, IEmailHandler emailHandler, 
            IVentasRepository ventasRepository, IComprasRepository comprasRepository)
        {
            _comprasRepository = comprasRepository;
            _ventasRepository = ventasRepository;
            _tokenHandler = tokenHandler;
            _emailHandler = emailHandler;
        }

        public void EnviarReporteCompraVentaAUsuarioLoggeado()
        {
            Guid empresaId = _tokenHandler.GetCurrentSessionInfo().EmpresaId;
            string reportRequesterEmail = _tokenHandler.GetCurrentSessionInfo().Email;
            string nombreEmpresa = _tokenHandler.GetCurrentSessionInfo().Empresa;

            List<Venta> ventas = _ventasRepository.ObtenerPorEmpresaId(empresaId, false);
            List<Compra> compras = _comprasRepository.ObtenerPorEmpresaId(empresaId);

            string subject = $"Reporte Compra|Venta de {nombreEmpresa}";
            string body = GetEmailBody(ventas, compras);

            _emailHandler.SendEmail(null, reportRequesterEmail, subject, body);
        }

        private string GetEmailBody(List<Venta> ventas, List<Compra> compras)
        {
            var ventasUltimoAnio = ventas.Where(v => v.FechaVenta.Year == DateTime.Today.Year && v.Realizada);
            var comprasUltimoAnio = compras.Where(v => v.FechaCompra.Year == DateTime.Today.Year);

            var mesYVentas= new Dictionary<int, int>();
            var mesYCompras= new Dictionary<int, int>();


            for(int i = 1; i <= 12; i++)
            {
                mesYVentas.Add(i, ventasUltimoAnio.Where(v => v.FechaVenta.Month == i).Count());
                mesYCompras.Add(i, comprasUltimoAnio.Where(v => v.FechaCompra.Month == i).Count());
            }

            string chartVentas = "<img src=\"https://quickchart.io/chart?c={type:'bar',data:{labels:["+ ConcatKeys(mesYVentas) +"],datasets:[{label:'Numero de Ventas por mes este año',data:["+ ConcatValues(mesYVentas) + "]}]}} \"/>";
            string chartCompras = "<img src=\"https://quickchart.io/chart?c={type:'bar',data:{labels:[" + ConcatKeys(mesYCompras) + "],datasets:[{label:'Numero de Compras por mes este año',data:[" + ConcatValues(mesYCompras) + "]}]}} \"/>";


            return chartVentas + chartCompras;
        }

        private string ConcatKeys(Dictionary<int, int>  diccionario)
        {
            bool first = true;
            string toReturn = "";

            foreach(KeyValuePair<int, int> entry in diccionario)
            {
                if (first)
                {
                    toReturn += entry.Key;
                    first = false;
                }
                else
                {
                    toReturn += "," + entry.Key;
                }
            }

            return toReturn;
        }

        private string ConcatValues(Dictionary<int, int> diccionario)
        {
            bool first = true;
            string toReturn = "";

            foreach (KeyValuePair<int, int> entry in diccionario)
            {
                if (first)
                {
                    toReturn += entry.Value;
                    first = false;
                }
                else
                {
                    toReturn += "," + entry.Value;
                }
            }

            return toReturn;
        }
    }
}
