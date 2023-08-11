using GESINV.ProductsService.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GESINV.ProductsService.PersistanceAccess.Interface
{
    public interface IProveedoresRepository
    {
        Proveedor Crear(Proveedor proveedor);
        Proveedor? ObtenerPorIdYEmpresaIdProveedorAccesible(Guid proveedorId, Guid empresaId);
        Proveedor Modificar(Proveedor proveedor);
        List<Proveedor> ObtenerPorEmpresaIdProveedoresAccesibles(Guid empresaId);
    }
}
