using GESINV.ProductsService.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GESINV.ProductsService.PersistanceAccess.Interface
{
    public interface IComprasRepository
    {
        Compra Crear(Compra compra);
        List<Compra> ObtenerPorEmpresaId(Guid empresaId);
        Compra? ObtenerPorIdYEmpresaId(Guid compraId, Guid empresaId);
        List<Compra> ObtenerPorEmpresaIdNombreProveedorYFecha(Guid empresaId, Guid proveedorId, DateTime? fechaDesde, DateTime? fechaHasta);
    }
}
