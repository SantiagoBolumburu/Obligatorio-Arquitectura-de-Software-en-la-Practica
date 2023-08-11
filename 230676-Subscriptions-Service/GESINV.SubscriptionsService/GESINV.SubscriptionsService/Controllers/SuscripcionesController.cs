using GESINV.IdentityHandler;
using GESINV.SubscriptionsService.Filters;
using GESINV.SubscriptionsService.Logic.Interface;
using GESINV.SubscriptionsService.Models;
using Microsoft.AspNetCore.Mvc;

namespace GESINV.SubscriptionsService.Controllers
{
    [ApiController]
    [Route("API/v2/suscripciones")]
    public class SuscripcionesController : Controller
    {
        private readonly ISuscripcionesCompraVentaLogic _suscripcionesCompraVentaLogic;
        private readonly ISuscripcionesStockLogic _suscripcionesStockLogic;

        public SuscripcionesController(ISuscripcionesCompraVentaLogic suscripcionesCompraVentaLogic,
            ISuscripcionesStockLogic suscripcionesStockLogic)
        {
            _suscripcionesStockLogic = suscripcionesStockLogic;
            _suscripcionesCompraVentaLogic = suscripcionesCompraVentaLogic;
        }


        [HttpGet]
        [AuthorizationAndIdentitySetupFilter(new string[] { Roles.RolAdministrador })]
        public IActionResult GetUserSuscripciones()
        {
            List<SubscriptionModelOut> subscriptionCompraVentaModelOuts = _suscripcionesCompraVentaLogic.ObtenerPorUsuarioEnSession();
            List<SubscriptionModelOut> subscriptionStockModelOuts = _suscripcionesStockLogic.ObtenerPorUsuarioEnSession();

            UsuarioSubscriptionOut usuarioSubscriptionOut = new UsuarioSubscriptionOut()
            {
                CompraVentaSubscriptions = subscriptionCompraVentaModelOuts,
                StockSubscription = subscriptionStockModelOuts
            };

            return Ok(usuarioSubscriptionOut);
        }

        [HttpPost("compraventa/{productoId}")]
        [AuthorizationAndIdentitySetupFilter(new string[] { Roles.RolAdministrador })]
        public IActionResult CreateSuscripcionCompraVenta(Guid productoId)
        {
            SubscriptionModelOut subscriptionModelOut = _suscripcionesCompraVentaLogic.Crear(productoId);

            return Ok(subscriptionModelOut);
        }

        [HttpGet("compraventa")]
        [AuthorizationAndIdentitySetupFilter(new string[] { Roles.RolAdministrador })]
        public IActionResult GetUserSuscripcionCompraVenta()
        {
            List<SubscriptionModelOut> subscriptionModelOuts = _suscripcionesCompraVentaLogic.ObtenerPorUsuarioEnSession();

            return Ok(subscriptionModelOuts);
        }

        [HttpDelete("compraventa/{productoId}")]
        [AuthorizationAndIdentitySetupFilter(new string[] { Roles.RolAdministrador })]
        public IActionResult RemoverSuscripcionCompraVenta(Guid productoId)
        {
            _suscripcionesCompraVentaLogic.Eliminar(productoId);

            return Ok();
        }


        [HttpPost("stock/{productoId}")]
        [AuthorizationAndIdentitySetupFilter(new string[] { Roles.RolAdministrador })]
        public IActionResult CrearSuscripcionStock(Guid productoId)
        {
            SubscriptionModelOut subscriptionModelOut = _suscripcionesStockLogic.Crear(productoId);

            return Ok(subscriptionModelOut);
        }

        [HttpGet("stock")]
        [AuthorizationAndIdentitySetupFilter(new string[] { Roles.RolAdministrador })]
        public IActionResult GetUserSuscripcionStock()
        {
            List<SubscriptionModelOut> subscriptionModelOuts = _suscripcionesStockLogic.ObtenerPorUsuarioEnSession();

            return Ok(subscriptionModelOuts);
        }

        [HttpDelete("stock/{productoId}")]
        [AuthorizationAndIdentitySetupFilter(new string[] { Roles.RolAdministrador })]
        public IActionResult RemoverSuscripcionStock(Guid productoId)
        {
            _suscripcionesStockLogic.Eliminar(productoId);

            return Ok();
        }
    }
}
