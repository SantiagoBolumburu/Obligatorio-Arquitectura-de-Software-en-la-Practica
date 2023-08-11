using GESINV.IdentityHandler;
using GESINV.ProductsService.Filters;
using GESINV.ProductsService.Logic.Interface;
using GESINV.ProductsService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GESINV.ProductsService.Controllers
{
    [ApiController]
    [Route("API/v2/compras")]
    public class ComprasController : Controller
    {
        private IComprasLogic _comprasLogic;

        public ComprasController(IComprasLogic comprasLogic)
        {
            _comprasLogic = comprasLogic;
        }

        [HttpPost]
        [AuthorizationAndIdentitySetupFilter(new string[] { Roles.RolAdministrador })]
        public IActionResult CrearCompra([FromBody] CompraModel compraModel)
        {
            CompraModelOut compra = _comprasLogic.CrearCompraEnEmpresaDeUsuarioLoggeado(compraModel);

            return Ok(compra);
        }

        [HttpGet]
        [AuthorizationAndIdentitySetupFilter(new string[] { Roles.RolAdministrador })]
        public IActionResult ObtenerTodasLasCompras()
        {
            List<CompraModelOut> compra = _comprasLogic.ObtenerTodasLasComprasDeLaEmpresaDelUsaurioLoggeado();

            return Ok(compra);
        }

        [HttpGet("{compraId}")]
        [AuthorizationAndIdentitySetupFilter(new string[] { Roles.RolAdministrador })]
        public IActionResult ObtenerCompra(Guid compraId)
        {
            CompraModelOut compra = _comprasLogic.ObtenerCompraDeLaEmpresaDeUsuarioLoggeado(compraId);

            return Ok(compra);
        }

        [HttpGet("proveedor/{proveedorId}")]
        [AuthorizationAndIdentitySetupFilter(new string[] { Roles.RolAdministrador, Roles.RolAplicacion })]
        public IActionResult ObtenerComprasDeProveedor(Guid proveedorId, [FromQuery] DateTime? fechaDesde, [FromQuery] DateTime? fechaHasta)
        {
            List<CompraModelOut> compras = _comprasLogic.ObtenerComprasAProveedorDeLaEmpresa(proveedorId, fechaDesde, fechaHasta);

            return Ok(compras);
        }

        [HttpGet("setup")]
        [AuthorizationAndIdentitySetupFilter(new string[] { Roles.RolAdministrador })]
        public IActionResult ObtenerSeUpDeCompra()
        {
            CompraSetUpModel setUp = _comprasLogic.ObtenerCompraSetUpsDeEmpresa();

            return Ok(setUp);
        }
    }
}
