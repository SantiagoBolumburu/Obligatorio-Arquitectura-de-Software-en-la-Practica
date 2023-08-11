using GESINV.Email.Service.Clases;
using GESINV.Email.Service.Domain;
using GESINV.Email.Service.Domain.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace GESINV.Email.Service.Controllers
{
    [ApiController]
    [Route("API/v2/health")]
    public class HealthController : Controller
    {
        [HttpGet]
        public IActionResult SendEmail()
        {
            string envVarsDiagnose = GetEnvVarsDiagnostic();

            return Ok(new
            {
                EnvVars = envVarsDiagnose,
            });
        }

        private string GetEnvVarsDiagnostic()
        {
            string[] envVars =
            {
                Environment.GetEnvironmentVariable(EnvarionmentVariablesNames.GESINV_SMTP_MAIL) ?? "",
                Environment.GetEnvironmentVariable(EnvarionmentVariablesNames.GESINV_SMTP_PASSWORD) ?? "",
                Environment.GetEnvironmentVariable(EnvarionmentVariablesNames.GESINV_SMTP_HOST) ?? ""
            };
        
            int missingCount = envVars.Count(val => val.Equals(""));

            string envVarsHelthDrecription;
            if (missingCount == 0)
                envVarsHelthDrecription = "Ok";
            else
                envVarsHelthDrecription = $"Number empty/missing: {missingCount}";

            return envVarsHelthDrecription;
        }
    }
}
        