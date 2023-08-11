using GESINV.ProductsService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GESINV.ProductsService.Logic.Interface
{
    public interface IComprasLogic
    {
        CompraModelOut CrearCompraEnEmpresaDeUsuarioLoggeado(CompraModel compraModel);
        List<CompraModelOut> ObtenerTodasLasComprasDeLaEmpresaDelUsaurioLoggeado();
        CompraModelOut? ObtenerCompraDeLaEmpresaDeUsuarioLoggeado(Guid compraId);
        List<CompraModelOut> ObtenerComprasAProveedorDeLaEmpresa(Guid proveedorId, DateTime? fechaDesde, DateTime? fechaHasta);
        CompraSetUpModel ObtenerCompraSetUpsDeEmpresa();
    }
}
