using GESINV.AthenticationService.BackingServicesAccess.Abstractions;
using GESINV.AthenticationService.Filters;
using GESINV.AthenticationService.PersistanceAccess.Interface;
using GESINV.AthenticationService.Utils;
using Microsoft.AspNetCore.Mvc;

namespace GESINV.AthenticationService.Controllers
{
    [ApiController]
    [Route("API/v2/health")]
    public class HealthController : Controller
    {
        private readonly IEmailSender _emailSender;
        private readonly IKeyValueStorage _keyValueStorage;
        private readonly IPersistanceConnectionChecker _persistanceConnectionChecker;

        public HealthController(IEmailSender emailSender, IKeyValueStorage keyValueStorage,
            IPersistanceConnectionChecker persistanceConnectionChecker)
        {
            _emailSender = emailSender;
            _keyValueStorage = keyValueStorage;
            _persistanceConnectionChecker = persistanceConnectionChecker;
        }

        [HttpGet]
        public IActionResult SessionIsActiveAndValid()
        {
            string envVarsDiagnose = GetEnvVarsDiagnostic();
            string emailSenderDiagnose = CanContact_EmailSender() ? "Ok" : "Can't make contact";  
            string keyValueStorageDiagnose = CanContact_KeyValueStorage() ? "Ok" : "Can't make contact";
            string pesistanceConnectionDiagnose = CanContact_PersistanceConnection() ? "Ok" : "Can't make contact";

            return Ok(new {
                EnvVars = envVarsDiagnose,
                BackingService_EmailService = emailSenderDiagnose,
                BackingService_KeyValueStorage = keyValueStorageDiagnose,
                BackingService_DataBase = pesistanceConnectionDiagnose
            });
        }

        private string GetEnvVarsDiagnostic()
        {
            string[] envVars =
            {
                Environment.GetEnvironmentVariable(EnvarionmentVariablesNames.GESINV_AUTHENTICATION_DB_CONNECTION_STRING) ?? "",
                Environment.GetEnvironmentVariable(EnvarionmentVariablesNames.GESINV_FRONTEND_LINK_INVITACIONES) ?? "",
                Environment.GetEnvironmentVariable(EnvarionmentVariablesNames.GESINV_URL_HTTP_API_EMAILSERVICE) ?? "",
                Environment.GetEnvironmentVariable(EnvarionmentVariablesNames.GESINV_JWT_SECRET) ?? ""
            };  
                
            int missingCount = envVars.Count(val => val.Equals(""));

            string envVarsHelthDrecription;
            if (missingCount == 0)
                envVarsHelthDrecription = "Ok";
            else
                envVarsHelthDrecription = $"Number empty/missing: {missingCount}";

            return envVarsHelthDrecription;
        }

        private bool CanContact_KeyValueStorage()
        {
            return _keyValueStorage.GetHealth();
        }

        private bool CanContact_EmailSender()
        {
            return _emailSender.GetHealth();
        }

        private bool CanContact_PersistanceConnection()
        {
            return _persistanceConnectionChecker.CanConnect();
        }
    }
}
