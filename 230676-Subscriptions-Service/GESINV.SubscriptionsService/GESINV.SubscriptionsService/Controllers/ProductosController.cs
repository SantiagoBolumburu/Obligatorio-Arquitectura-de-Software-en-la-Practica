using GESINV.SubscriptionsService.Dominio;
using GESINV.SubscriptionsService.Filters;
using GESINV.SubscriptionsService.Logic.Interface;
using GESINV.SubscriptionsService.Models;
using Microsoft.AspNetCore.Mvc;

namespace GESINV.SubscriptionsService.Controllers
{
    [ApiController]
    [Route("API/v2/productos")]
    public class ProductosController : Controller
    {
        private readonly IProductoLogic _productoLogic;

        public ProductosController(IProductoLogic productoLogic)
        {
            _productoLogic = productoLogic;
        }


        [HttpPost]
        [AuthorizationAndIdentitySetupFilter(new string[] { })]
        public IActionResult CreateProducto([FromBody] ProductoModelIn productoModel)
        {
            ProductoModelOut productoModelOut = _productoLogic.Agregar(productoModel);

            return Ok(productoModelOut);
        }
    }
}
