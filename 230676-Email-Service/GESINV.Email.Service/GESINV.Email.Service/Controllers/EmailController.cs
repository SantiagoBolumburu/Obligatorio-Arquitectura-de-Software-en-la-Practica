using GESINV.Email.Service.Domain;
using GESINV.Email.Service.Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GESINV.Email.Service.Controllers
{
    [ApiController]
    [Route("API/v2/emails")]
    public class EmailController : Controller
    {
        private readonly IEmailHandler _emailHandler;
        private readonly IConfiguration _configuration;

        public EmailController(IEmailHandler emailHandler, IConfiguration configuration) 
        {
            _emailHandler = emailHandler;
            _configuration = configuration;
        }


        [HttpPost]
        public IActionResult SendEmail([FromBody] EmailConfig emailConfig)
        {
            string fromMail = emailConfig.FromMail ?? _configuration.GetValue<string>("Emails:Defaults:FromMail");
            string toMail = emailConfig.ToMail ?? _configuration.GetValue<string>("Emails:Defaults:ToMail");
            string subject = emailConfig.Subject ?? _configuration.GetValue<string>("Emails:Defaults:Subject");
            string body = emailConfig.Body ?? _configuration.GetValue<string>("Emails:Defaults:Body");

            _emailHandler.SendEmail(fromMail, toMail, subject, body);

            return Ok();
        }
    }
}
