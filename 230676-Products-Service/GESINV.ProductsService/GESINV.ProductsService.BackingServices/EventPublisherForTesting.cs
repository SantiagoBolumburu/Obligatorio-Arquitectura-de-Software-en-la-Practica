using GESINV.ProductsService.BackingServices.Interface;
using GESINV.ProductsService.BackingServices.Interface.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GESINV.ProductsService.BackingServices
{
    public class EventPublisherForTesting : IEventPublisher
    {
        public bool GetHealth()
        {
            return true;
        }

        public void PublishEvent(ProductoEventInfo info)
        {
            Console.WriteLine(JsonSerializer.Serialize(info));
        }

        public void PublishNuevoProducto(NuevoProducto nuevoProducto)
        {
            Console.WriteLine(JsonSerializer.Serialize(nuevoProducto));
        }
    }
}
