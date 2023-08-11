using GESINV.IdentityHandler;
using GESINV.IdentityHandler.Abstractions;
using GESINV.ProductsService.BackingServices.Interface;
using GESINV.ProductsService.BackingServices.Interface.Clases;
using GESINV.ProductsService.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace GESINV.ProductsService.BackingServices
{
    public class HttpEventPublisher : IEventPublisher
    {
        private readonly ITokenHandler _tokenHandler;

        public HttpEventPublisher(ITokenHandler tokenHandler) 
        {
            _tokenHandler = tokenHandler;
        }

        public bool GetHealth()
        {
            try
            {
                string subscriptionsServiceApiUrl = 
                    Environment.GetEnvironmentVariable(EnvarionmentVariablesNames.GESINV_URL_HTTP_API_SUBSCRIPTIONSSERVICE)
                    ?? throw new Exception("Env variable missing");

                HttpClient httpClient = new HttpClient()
                {
                    BaseAddress = new Uri(subscriptionsServiceApiUrl)
                };

                HttpResponseMessage response = httpClient.GetAsync("health").Result;

                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public void PublishEvent(ProductoEventInfo info)
        {
            try
            {
                string subscriptionsServiceApiUrl = Environment.GetEnvironmentVariable(EnvarionmentVariablesNames.GESINV_URL_HTTP_API_SUBSCRIPTIONSSERVICE)
                    ?? throw new Exception("Env variable missing");

                string token = GetSessionToken();

                HttpClient httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(subscriptionsServiceApiUrl);
                httpClient.DefaultRequestHeaders.Add(UsedHttpHeaders.AUTHORIZATION, token);

                HttpResponseMessage response = httpClient.PostAsJsonAsync("eventos", info).Result;

                response.EnsureSuccessStatusCode();
            }
            catch (Exception)
            {

            }
        }

        public void PublishNuevoProducto(NuevoProducto nuevoProducto)
        {
            try
            {
                string subscriptionsServiceApiUrl = Environment.GetEnvironmentVariable(EnvarionmentVariablesNames.GESINV_URL_HTTP_API_SUBSCRIPTIONSSERVICE)
                ?? throw new Exception("Env variable missing");

                string token = GetSessionToken();

                HttpClient httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(subscriptionsServiceApiUrl);
                httpClient.DefaultRequestHeaders.Add(UsedHttpHeaders.AUTHORIZATION, token);

                HttpResponseMessage response = httpClient.PostAsJsonAsync("productos", nuevoProducto).Result;

                response.EnsureSuccessStatusCode();
            }
            catch (Exception)
            {

            }
        }

        private string GetSessionToken()
        {
            string? sessionToken = _tokenHandler.GetCurrentRequestToken(UsedHttpHeaders.AUTHORIZATION, TokenFormat.BEARER);

            if (sessionToken == null)
            {
                string jwtSecret = Environment.GetEnvironmentVariable(EnvarionmentVariablesNames.GESINV_JWT_SECRET)
                    ?? throw new Exception("Env variable missing");

                sessionToken = _tokenHandler.GetGesinvApplicationToken(TokenFormat.BEARER, jwtSecret);
            }

            return sessionToken;
        }
    }
}
