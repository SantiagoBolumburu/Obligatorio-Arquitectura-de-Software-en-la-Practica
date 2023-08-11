using GESINV.ProductsService.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GESINV.ProductsService.PersistanceAccess.Interface
{
    public interface IVentasRepository
    {
        Venta Crear(Venta venta);
        Venta Actualizar(Venta venta);
        List<Venta> ObtenerPorFechaHastaInclusive(DateTime dateTime, bool programada, bool realizada);
        List<Venta> ObtenerPorEmpresaIdPaginado(Guid empresaId, DateTime fechaDesde, DateTime fechaHasta,
                                                   int indicePagina, int cantidadPorPagina, bool realizada);
        List<Venta> ObtenerPorEmpresaId(Guid empresaId, bool programadas);
        Venta? ObtenerPorIdYEmpresaId(Guid ventaId, Guid empresaId);
    }
}
