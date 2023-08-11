using GESINV.AthenticationService.BackingServicesAccess.Abstractions;
using GESINV.AthenticationService.BackingServicesAccess.Clases;
using GESINV.AthenticationService.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GESINV.AthenticationService.BackingServicesAccess
{
    public class HttpEmailSender : IEmailSender
    {
        public void EnviarCorreo(string fromEmail, string toEmail, string subject, string body)
        {
            try
            {
                string emailServiceApiUrl = Environment.GetEnvironmentVariable(EnvarionmentVariablesNames.GESINV_URL_HTTP_API_EMAILSERVICE)
                    ?? throw new Exception("Env variable missing");

                HttpClient httpClient = new HttpClient()
                {
                    BaseAddress = new Uri(emailServiceApiUrl)
                };

                HttpEmailConfig config = new HttpEmailConfig()
                {
                    FromMail = fromEmail,
                    ToMail = toEmail,
                    Subject = subject,
                    Body = body
                };

                HttpResponseMessage response = httpClient.PostAsJsonAsync("emails", config).Result;

                response.EnsureSuccessStatusCode();
            }
            catch (Exception)
            {

            }
        }

        public bool GetHealth()
        {
            try
            {
                string emailServiceApiUrl = Environment.GetEnvironmentVariable(EnvarionmentVariablesNames.GESINV_URL_HTTP_API_EMAILSERVICE)
                ?? throw new Exception("Env variable missing");

                HttpClient httpClient = new HttpClient()
                {
                    BaseAddress = new Uri(emailServiceApiUrl)
                };

                HttpResponseMessage response = httpClient.GetAsync("health").Result;

                response.EnsureSuccessStatusCode();

            }
            catch(Exception ex)
            {
                return false;
            }
            return true;
        }
    }
}