using GESINV.IdentityHandler;
using GESINV.ProductsService.Filters;
using GESINV.ProductsService.Logic;
using GESINV.ProductsService.Logic.Interface;
using GESINV.ProductsService.Models;
using Microsoft.AspNetCore.Mvc;

namespace GESINV.ProductsService.Controllers
{
    [ApiController]
    [Route("API/v2/ventasprogramadas")]
    public class VentasProgramadasController : Controller
    {
        private IVentasLogic _ventasLogic;

        public VentasProgramadasController(IVentasLogic ventasLogic)
        {
            _ventasLogic = ventasLogic;
        }


        [HttpPost]
        [AuthorizationAndIdentitySetupFilter(new string[] { Roles.RolAdministrador, Roles.RolEmpleado })]
        public IActionResult CrearVentaProgramada([FromBody] VentaModel ventaProgramadaModel)
        {
            VentaModelOut compra = _ventasLogic.CrearVentaEnEmpresaDeUsuarioLoggeado(ventaProgramadaModel, true);

            return Ok(compra);
        }

        [HttpGet]
        [AuthorizationAndIdentitySetupFilter(new string[] { Roles.RolAdministrador })]
        public IActionResult ObtenerTodasLasVentasProgramadas()
        {
            List<VentaModelOut> compra = _ventasLogic.ObtenerTodasLasVentaProgramadaDeLaEmpresaDelUsaurioLoggeado();

            return Ok(compra);
        }
    }
}
