using GESINV.ProductsService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GESINV.ProductsService.Logic.Interface
{
    public interface IProveedoresLogic
    {
        ProveedorModelOut CrearProveedor(ProveedorModel proveedorModel);
        List<ProveedorModelOut> ObtenerTodosLosProveedoresDeLaEmpresaDeUsuarioLoggeado();
        ProveedorModelOut? ObtenerProveedorDeLaEmpresaDeUsuarioLoggeado(Guid proveedorId);
        ProveedorModelOut ModificarProveedor(Guid proveedorId, ProveedorModel proveedorModel);
        void EliminarProveedor(Guid proveedorId);
    }
}
