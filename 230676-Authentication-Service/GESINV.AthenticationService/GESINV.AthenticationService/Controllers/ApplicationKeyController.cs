using GESINV.AthenticationService.Dominio;
using GESINV.AthenticationService.Filters;
using GESINV.AthenticationService.Logic.Interface;
using Microsoft.AspNetCore.Mvc;

namespace GESINV.AthenticationService.Controllers
{
    [ApiController]
    [Route("API/v2/appkey")]
    public class ApplicationKeyController : Controller
    {
        IAppkeyLogic _appkeyLogic;

        public ApplicationKeyController(IAppkeyLogic appkeyLogic)
        {
            _appkeyLogic = appkeyLogic;
        }
       

        [HttpPost]
        [AuthorizationAndSessionSetUpFilter(new string[] { Roles.RolAdministrador })]
        public IActionResult CrearApikey()
        {
            string apPkeyString = _appkeyLogic.CrearAppKey();

            return Ok(new { appkey = apPkeyString });
        }

        [HttpGet]
        [AuthorizationAndSessionSetUpFilter(new string[] { Roles.RolAdministrador })]
        public IActionResult ObtenerAppkey()
        {
            string apPkeyString = _appkeyLogic.ObtenerAppkeyActual();

            return Ok(new { appkey = apPkeyString });
        }

        [HttpDelete]
        [AuthorizationAndSessionSetUpFilter(new string[] { Roles.RolAdministrador })]
        public IActionResult DeleteApikey()
        {
            _appkeyLogic.TeminarApiKey();

            return Ok();
        }
    }
}
