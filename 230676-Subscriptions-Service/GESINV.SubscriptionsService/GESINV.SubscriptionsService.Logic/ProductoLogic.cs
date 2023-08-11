using GESINV.IdentityHandler.Abstractions;
using GESINV.SubscriptionsService.DataAccess.Interface;
using GESINV.SubscriptionsService.Dominio;
using GESINV.SubscriptionsService.Dominio.Utils;
using GESINV.SubscriptionsService.Logic.Interface;
using GESINV.SubscriptionsService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GESINV.SubscriptionsService.Logic
{
    public class ProductoLogic : IProductoLogic
    {
        public readonly ITokenHandler _tokenHandler;
        public readonly IProductosRepository _productoRepository;

        public ProductoLogic(ITokenHandler tokenHandler, IProductosRepository productoRepository)
        {
            _tokenHandler = tokenHandler;
            _productoRepository = productoRepository;
        }

        public ProductoModelOut Agregar(ProductoModelIn productoModel)
        {
            Guid empresaId = _tokenHandler.GetCurrentSessionInfo().EmpresaId;

            Producto producto = new Producto()
            {
                Id = Guid.NewGuid(),
                EmpresaId = empresaId,
                ProductoMainId = productoModel.ProductoId
            };

            Producto productoDB = _productoRepository.Crear(producto);

            return DominioAModel.Convertir(productoDB);
        }
    }
}
