using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GESINV.ProductsService.Utils
{
    public static class EnvarionmentVariablesNames
    {
        public static string GESINV_JWT_SECRET = "GESINV_JWT_SECRET";
        public static string GESINV_PRODUCTS_DB_CONNECTION_STRING = "GESINV_PRODUCTS_DB_CONNECTION_STRING";
        public static string GESINV_URL_HTTP_API_EMAILSERVICE = "GESINV_URL_HTTP_API_EMAILSERVICE";
        public static string GESINV_URL_HTTP_API_SUBSCRIPTIONSSERVICE = "GESINV_URL_HTTP_API_SUBSCRIPTIONSSERVICE";

        public static string[] GetAllEnvVarNames()
        {
            return new string[] { GESINV_JWT_SECRET, GESINV_PRODUCTS_DB_CONNECTION_STRING, 
                GESINV_URL_HTTP_API_EMAILSERVICE, GESINV_URL_HTTP_API_SUBSCRIPTIONSSERVICE };
        }
    }
}
