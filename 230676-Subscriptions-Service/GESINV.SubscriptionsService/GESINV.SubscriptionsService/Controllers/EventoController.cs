using GESINV.SubscriptionsService.Filters;
using GESINV.SubscriptionsService.Logic;
using GESINV.SubscriptionsService.Logic.Interface;
using GESINV.SubscriptionsService.Models;
using Microsoft.AspNetCore.Mvc;

namespace GESINV.SubscriptionsService.Controllers
{
    [ApiController]
    [Route("API/v2/eventos")]
    public class EventoController : Controller
    {
        private readonly IEventosLogic _eventosLogic;

        public EventoController(IEventosLogic eventosLogic)
        {
            _eventosLogic = eventosLogic;
        }

        [HttpPost]
        [AuthorizationAndIdentitySetupFilter(new string[] { })]
        public IActionResult CreateProducto([FromBody] EventoModelIn eventoModelIn)
        {
            _eventosLogic.Agregar(eventoModelIn);

            return Ok();
        }
    }
}
