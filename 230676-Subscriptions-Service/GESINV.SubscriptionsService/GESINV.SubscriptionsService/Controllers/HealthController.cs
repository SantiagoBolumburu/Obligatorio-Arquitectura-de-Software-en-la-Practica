using GESINV.SubscriptionsService.BackingServices.Abstractions;
using GESINV.SubscriptionsService.DataAccess.Interface;
using GESINV.SubscriptionsService.Utils;
using Microsoft.AspNetCore.Mvc;

namespace GESINV.SubscriptionsService.Controllers
{
    [ApiController]
    [Route("API/v2/health")]
    public class HealthController : Controller
    {
        private readonly IEmailHandler _emailHandler;
        private readonly IPersistanceConnectionChecker _persistanceConnectionChecker;

        public HealthController(IEmailHandler emailHandler, IPersistanceConnectionChecker persistanceConnectionChecker)
        {
            _emailHandler = emailHandler;
            _persistanceConnectionChecker = persistanceConnectionChecker;
        }

        [HttpGet]
        public IActionResult SessionIsActiveAndValid()
        {
            string envVarsDiagnose = GetEnvVarsDiagnostic();
            string emailSenderDiagnose = _emailHandler.GetHealth() ? "Ok" : "Can't make contact";
            string pesistanceConnectionDiagnose = _persistanceConnectionChecker.CanConnect() ? "Ok" : "Can't make contact";

            return Ok(new
            {
                EnvVars = envVarsDiagnose,
                BackingService_EmailService = emailSenderDiagnose,
                BackingService_DataBase = pesistanceConnectionDiagnose
            });
        }

        private string GetEnvVarsDiagnostic()
        {
            string[] envVarNames = EnvarionmentVariablesNames.GetAllEnvVarNames();

            string?[] envVars = envVarNames.Select(v => Environment.GetEnvironmentVariable(v)).ToArray()
                ?? throw new Exception("Error obteniendo env vars");

            int missingCount = envVars.Count(val => string.IsNullOrEmpty(val));

            string envVarsHelthDrecription;
            if (missingCount == 0)
                envVarsHelthDrecription = "Ok";
            else
                envVarsHelthDrecription = $"Amount that are empty/missing: {missingCount}";

            return envVarsHelthDrecription;
        }
    }
}
