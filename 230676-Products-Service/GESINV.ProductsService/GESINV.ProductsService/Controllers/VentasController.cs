using GESINV.IdentityHandler;
using GESINV.ProductsService.Filters;
using GESINV.ProductsService.Logic.Interface;
using GESINV.ProductsService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GESINV.ProductsService.Controllers
{
    [ApiController]
    [Route("API/v2/ventas")]
    public class VentasController : Controller
    {
        private IVentasLogic _ventasLogic;

        public VentasController(IVentasLogic ventasLogic)
        {
            _ventasLogic = ventasLogic;
        }

        [HttpPost]
        [AuthorizationAndIdentitySetupFilter(new string[] { Roles.RolAdministrador, Roles.RolEmpleado })]
        public IActionResult CrearVenta([FromBody] VentaModel ventaModel)
        {
            VentaModelOut venta = _ventasLogic.CrearVentaEnEmpresaDeUsuarioLoggeado(ventaModel, false);

            return Ok(venta);
        }

        [HttpGet]
        [AuthorizationAndIdentitySetupFilter(new string[] { Roles.RolAdministrador, Roles.RolEmpleado })]
        public IActionResult ObtenerTodasLasVentas([FromQuery] DateTime? fechaDesde, [FromQuery] DateTime? fechaHasta,
                                                   [FromQuery] int? indicePagina, [FromQuery] int? cantidadPorPagina)
        {

            List<VentaModelOut> ventasCantPaginas = _ventasLogic.ObtenerVentasDeLaEmpresaDelUsaurioLoggeado(fechaDesde, fechaHasta, indicePagina, cantidadPorPagina);

            return Ok(ventasCantPaginas);
        }

        [HttpGet("{ventaId}")]
        [AuthorizationAndIdentitySetupFilter(new string[] { Roles.RolAdministrador, Roles.RolEmpleado })]
        public IActionResult ObtenerVenta(Guid ventaId)
        {
            VentaModelOut venta = _ventasLogic.ObtenerVentaDeLaEmpresaDeUsuarioLoggeado(ventaId);

            return Ok(venta);
        }
    }
}
