using GESINV.ProductsService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GESINV.ProductsService.Logic.Interface
{
    public interface IVentasLogic
    {
        VentaModelOut CrearVentaEnEmpresaDeUsuarioLoggeado(VentaModel ventaModel, bool esProgramada);
        List<VentaModelOut> ObtenerVentasDeLaEmpresaDelUsaurioLoggeado(DateTime? fechaDesde, DateTime? fechaHasta,
                                                   int? indicePagina, int? cantidadPorPagina);
        VentaModelOut? ObtenerVentaDeLaEmpresaDeUsuarioLoggeado(Guid productoId);
        List<VentaModelOut> ObtenerTodasLasVentaProgramadaDeLaEmpresaDelUsaurioLoggeado();
        void RealizarVentasProgramadas();
    }
}
