using GESINV.IdentityHandler;
using GESINV.ProductsService.Filters;
using GESINV.ProductsService.Logic.Interface;
using GESINV.ProductsService.Models;
using Microsoft.AspNetCore.Mvc;

namespace GESINV.ProductsService.Controllers
{
    [ApiController]
    [Route("API/v2/reportes")]
    public class ReportesController : Controller
    {
        private readonly IReportesLogic _reportesLogic;

        public ReportesController(IReportesLogic reportesLogic) 
        {
            _reportesLogic = reportesLogic;
        }

        [HttpPost("comprasyventas")]
        [AuthorizationAndIdentitySetupFilter(new string[] { Roles.RolAdministrador, Roles.RolEmpleado })]
        public IActionResult CrearVenta()
        {
            _reportesLogic.EnviarReporteCompraVentaAUsuarioLoggeado();

            return Ok();
        }
    }
}
