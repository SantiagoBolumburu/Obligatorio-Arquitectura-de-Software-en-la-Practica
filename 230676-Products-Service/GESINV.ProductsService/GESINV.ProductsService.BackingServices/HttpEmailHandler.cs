﻿using GESINV.ProductsService.BackingServices.Clases;
using GESINV.ProductsService.BackingServices.Interface;
using GESINV.ProductsService.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace GESINV.ProductsService.BackingServices
{
    public class HttpEmailHandler : IEmailHandler
    {
        public void SendEmail(string? fromMail, string toMail, string subject, string body)
        {
            string emailServiceApiUrl = Environment.GetEnvironmentVariable(EnvarionmentVariablesNames.GESINV_URL_HTTP_API_EMAILSERVICE)
                ?? throw new Exception("Env variable missing");

            HttpClient httpClient = new HttpClient()
            {
                BaseAddress = new Uri(emailServiceApiUrl)
            };

            HttpEmailConfig config = new HttpEmailConfig()
            {
                FromMail = fromMail,
                ToMail = toMail,
                Subject = subject,
                Body = body
            };

            HttpResponseMessage response = httpClient.PostAsJsonAsync("emails", config).Result;

            response.EnsureSuccessStatusCode();
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
