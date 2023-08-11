using GESINV.IdentityHandler;
using GESINV.ProductsService.Filters;
using GESINV.ProductsService.Logic.Interface;
using GESINV.ProductsService.Models;
using Microsoft.AspNetCore.Mvc;

namespace GESINV.ProductsService.Controllers
{
    [ApiController]
    [Route("API/v2/proveedores")]
    public class ProveedoresController : Controller
    {
        private IProveedoresLogic _proveedoresLogic;

        public ProveedoresController(IProveedoresLogic proveedoresLogic)
        {
            _proveedoresLogic = proveedoresLogic;
        }

        [HttpPost]
        [AuthorizationAndIdentitySetupFilter(new string[] { Roles.RolAdministrador })]
        public IActionResult CrearProveedor([FromBody] ProveedorModel proveedorModel)
        {
            ProveedorModelOut proveedor = _proveedoresLogic.CrearProveedor(proveedorModel);

            return Ok(proveedor);
        }

        [HttpGet]
        [AuthorizationAndIdentitySetupFilter(new string[] { Roles.RolAdministrador })]
        public IActionResult ObtenerTodosLosProveedores()
        {
            List<ProveedorModelOut> proveedor = _proveedoresLogic.ObtenerTodosLosProveedoresDeLaEmpresaDeUsuarioLoggeado();

            return Ok(proveedor);
        }

        [HttpGet("{proveedorId}")]
        [AuthorizationAndIdentitySetupFilter(new string[] { Roles.RolAdministrador })]
        public IActionResult ObtenerProveedor(Guid proveedorId)
        {
            ProveedorModelOut proveedor = _proveedoresLogic.ObtenerProveedorDeLaEmpresaDeUsuarioLoggeado(proveedorId);

            return Ok(proveedor);
        }

        [HttpPut("{proveedorId}")]
        [AuthorizationAndIdentitySetupFilter(new string[] { Roles.RolAdministrador })]
        public IActionResult ModificarProveedor(Guid proveedorId, [FromBody] ProveedorModel proveedorModel)
        {
            ProveedorModelOut proveedor = _proveedoresLogic.ModificarProveedor(proveedorId, proveedorModel);

            return Ok(proveedor);
        }

        [HttpDelete("{proveedorId}")]
        [AuthorizationAndIdentitySetupFilter(new string[] { Roles.RolAdministrador })]
        public IActionResult EliminarProveedor(Guid proveedorId)
        {
            _proveedoresLogic.EliminarProveedor(proveedorId);

            return Ok();
        }
    }
}
