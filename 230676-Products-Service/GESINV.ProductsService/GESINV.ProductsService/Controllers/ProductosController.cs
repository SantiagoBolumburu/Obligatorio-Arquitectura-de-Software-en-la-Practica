using GESINV.IdentityHandler;
using GESINV.ProductsService.Filters;
using GESINV.ProductsService.Logic.Interface;
using GESINV.ProductsService.Models;
using Microsoft.AspNetCore.Mvc;

namespace GESINV.ProductsService.Controllers
{
    [ApiController]
    [Route("API/v2/productos")]
    public class ProductosController : Controller
    {
        private IProductosLogic _productosLogic;

        public ProductosController(IProductosLogic productosLogic)
        {
            _productosLogic = productosLogic;
        }

        
        [HttpPost]
        [AuthorizationAndIdentitySetupFilter(new string[] { Roles.RolAdministrador })]
        public IActionResult CrearProducto([FromBody] ProductoModel productoModel)
        {
            ProductoModelOut producto = _productosLogic.CrearProducto(productoModel);

            return Ok(producto);
        }

        [HttpGet]
        [AuthorizationAndIdentitySetupFilter(new string[] { Roles.RolAdministrador, Roles.RolEmpleado })]
        public IActionResult ObtenerTodosLosProductos()
        {
            List<ProductoModelOut> producto = _productosLogic.ObtenerTodosLosProductosDeLaEmpresaDeUsuarioLoggeado();

            return Ok(producto);
        }

        [HttpGet("{productoId}")]
        [AuthorizationAndIdentitySetupFilter(new string[] { Roles.RolAdministrador })]
        public IActionResult ObtenerProducto(Guid productoId)
        {
            ProductoModelOut? producto = _productosLogic.ObtenerProductoDeLaEmpresaDeUsuarioLoggeado(productoId);

            return Ok(producto);
        }   

        [HttpPut("{productoId}")]
        [AuthorizationAndIdentitySetupFilter(new string[] { Roles.RolAdministrador })]
        public IActionResult ModificarProducto(Guid productoId, [FromBody] ProductoModel productoModel)
        {
            ProductoModelOut producto = _productosLogic.ModificarProducto(productoId, productoModel);

            return Ok(producto);
        }

        [HttpDelete("{productoId}")]
        [AuthorizationAndIdentitySetupFilter(new string[] { Roles.RolAdministrador })]
        public IActionResult EliminarProducto(Guid productoId)
        {
            _productosLogic.EliminarProducto(productoId);

            return Ok();
        }

        [HttpGet("masvendidos")]
        [AuthorizationAndIdentitySetupFilter(new string[] { Roles.RolAdministrador, Roles.RolAplicacion })]
        public IActionResult ObtenerProductosMasVendidos()
        {
            List<ProductoModelOut> productos = _productosLogic.ObtenerProductosMasVendidos(null);

            return Ok(productos);
        }

        [HttpPost("syncsubscripciones")]
        [AuthorizationAndIdentitySetupFilter(new string[] { Roles.RolAdministrador })]
        public IActionResult SyncProducto()
        {
            _productosLogic.EmitirTodosLosPorductos();

            return Ok();
        }
    }
}
