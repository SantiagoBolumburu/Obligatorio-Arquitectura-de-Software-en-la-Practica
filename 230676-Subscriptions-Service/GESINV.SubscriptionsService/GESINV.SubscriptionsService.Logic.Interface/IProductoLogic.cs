using GESINV.SubscriptionsService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GESINV.SubscriptionsService.Logic.Interface
{
    public interface IProductoLogic
    {
        ProductoModelOut Agregar(ProductoModelIn productoModel);
    }
}
