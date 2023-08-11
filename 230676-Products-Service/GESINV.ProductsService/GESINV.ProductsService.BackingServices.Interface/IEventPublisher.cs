using GESINV.ProductsService.BackingServices.Interface.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GESINV.ProductsService.BackingServices.Interface
{
    public interface IEventPublisher
    {
        void PublishEvent(ProductoEventInfo info);
        void PublishNuevoProducto(NuevoProducto nuevoProducto);
        bool GetHealth();
    }
}
