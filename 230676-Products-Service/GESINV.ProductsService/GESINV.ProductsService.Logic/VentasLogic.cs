using GESINV.IdentityHandler;
using GESINV.IdentityHandler.Abstractions;
using GESINV.ProductsService.BackingServices.Interface;
using GESINV.ProductsService.BackingServices.Interface.Clases;
using GESINV.ProductsService.Dominio;
using GESINV.ProductsService.Logic.Interface;
using GESINV.ProductsService.Logic.Utils;
using GESINV.ProductsService.Models;
using GESINV.ProductsService.PersistanceAccess.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GESINV.ProductsService.Logic
{
    public class VentasLogic : IVentasLogic
    {
        private readonly ITokenHandler _tokenHandler;
        private readonly IEventPublisher _eventPublisher;
        private readonly IProductosRepository _productosRepositorio;
        private readonly IVentasRepository _ventasRepositorio;


        public VentasLogic(ITokenHandler tokenHandler, IEventPublisher eventPublisher,
            IProductosRepository productosRepositorio, IVentasRepository ventasRepositorio)
        {
            _tokenHandler = tokenHandler;
            _eventPublisher = eventPublisher;
            _productosRepositorio = productosRepositorio;
            _ventasRepositorio = ventasRepositorio;
        }

        public VentaModelOut CrearVentaEnEmpresaDeUsuarioLoggeado(VentaModel ventaModel, bool esProgramada)
        {
            ventaModel.Validar();

            if (HayProductosRepetidos(ventaModel.ProductosYCantidad))
                throw new ArgumentException("Las ventas no pueden tener productos repetidos.");

            Guid empresaId = _tokenHandler.GetCurrentSessionInfo().EmpresaId;

            List<DetalleVentaProducto> ventasProductos = ObtenerVentasProductos_YValidarStock(ventaModel.ProductosYCantidad, empresaId);

            Venta venta = new Venta()
            {
                Id = Guid.NewGuid(),
                NombreCliente = ventaModel.NombreCliente,
                FechaVenta = ventaModel.FechaVenta,
                MontoTotalEnPesos = ventaModel.MontoTotalEnPesos,
                DetallesVentasProductos = ventasProductos,
                EmpresaId = empresaId,
                Realizada = !esProgramada,
                Programada = esProgramada
            };

            Dictionary<Guid, int> productosActualizar = venta.DetallesVentasProductos.ToDictionary(d => d.ProductoId, d => d.Cantidad);
            _productosRepositorio.UpdateCantidadVentas(productosActualizar);

            Venta ventaDb = _ventasRepositorio.Crear(venta);

            EmitirEvento(venta);

            VentaModelOut ventaModelDb = DomainToModel.CombertirAModel(ventaDb);

            return ventaModelDb;
        }

        public List<VentaModelOut> ObtenerVentasDeLaEmpresaDelUsaurioLoggeado(DateTime? fechaDesde, DateTime? fechaHasta,
                                                   int? indicePagina, int? cantidadPorPagina)
        {
            Guid empresaId = _tokenHandler.GetCurrentSessionInfo().EmpresaId;

            fechaDesde ??= DateTime.MinValue;
            fechaHasta ??= DateTime.MaxValue;
            indicePagina ??= 0;
            cantidadPorPagina ??= int.MaxValue;

            List<Venta> ventas = _ventasRepositorio.ObtenerPorEmpresaIdPaginado(empresaId, fechaDesde.Value,
                fechaHasta.Value, indicePagina.Value, cantidadPorPagina.Value, true);

            List<VentaModelOut> models = new List<VentaModelOut>();

            if (ventas != null)
                models = ventas.Select(p => DomainToModel.CombertirAModel(p)).ToList();

            return models;
        }

        public VentaModelOut? ObtenerVentaDeLaEmpresaDeUsuarioLoggeado(Guid ventaId)
        {
            Guid empresaId = _tokenHandler.GetCurrentSessionInfo().EmpresaId;

            Venta? venta = _ventasRepositorio.ObtenerPorIdYEmpresaId(ventaId, empresaId);

            VentaModelOut? model = null;

            if (venta is not null)
                model = DomainToModel.CombertirAModel(venta);

            return model;
        }

        public List<VentaModelOut> ObtenerTodasLasVentaProgramadaDeLaEmpresaDelUsaurioLoggeado()
        {
            Guid empresaId = _tokenHandler.GetCurrentSessionInfo().EmpresaId;

            List<Venta> ventas = _ventasRepositorio.ObtenerPorEmpresaId(empresaId, true);

            List<VentaModelOut> models = new List<VentaModelOut>();

            if (ventas != null)
                models = ventas.Select(p => DomainToModel.CombertirAModel(p)).ToList();

            return models;
        }

        public void RealizarVentasProgramadas()
        {
            List<Venta> ventasProgramadasNoRealizadas = _ventasRepositorio.ObtenerPorFechaHastaInclusive(DateTime.Now, true, false);

            ventasProgramadasNoRealizadas.ForEach(p => {
                p.Realizada = true;
                _ventasRepositorio.Actualizar(p);
                EmitirEvento(p);
            });
        }


        private bool HayProductosRepetidos(List<Tuple<Guid, int>> idsCantidades)
        {
            HashSet<Guid> diffChecker = new HashSet<Guid>();
            bool todosDiferentes = idsCantidades.All(i => diffChecker.Add(i.Item1));

            return !todosDiferentes;
        }

        private List<DetalleVentaProducto> ObtenerVentasProductos_YValidarStock(List<Tuple<Guid, int>> tuplasIdCantidad, Guid empresaId)
        {
            List<Producto> productos = _productosRepositorio.GetByEmpresaId_ProductosAccesibles(empresaId);

            List<DetalleVentaProducto> ventasProductos = new List<DetalleVentaProducto>();

            foreach (Tuple<Guid, int> tupla in tuplasIdCantidad)
            {
                Producto producto = productos.FirstOrDefault(p => p.Id == tupla.Item1 && p.EmpresaId == empresaId)
                    ?? throw new ArgumentException("Uno de los productos ingresados en la compra no se encuantra en el sistema.");

                if (0 > (producto.CantidadEnInventario() - tupla.Item2))
                    throw new InvalidOperationException("No se puede realizar ventas de un producto por major cantidad a disponible en el inventario.");

                ventasProductos.Add(new DetalleVentaProducto()
                {
                    Id = Guid.NewGuid(),
                    Cantidad = tupla.Item2,
                    Producto = producto,
                    ProductoId = producto.Id,
                    StockDespuesDeVenta = producto.CantidadEnInventario() - tupla.Item2
                });
            }

            return ventasProductos;
        }


        private void EmitirEvento(Venta venta)
        {
            List<ProductoEventInfo> productoEventInfos;

            if (venta.Programada && !venta.Realizada)
            {
                productoEventInfos = venta.DetallesVentasProductos.Select(d => new ProductoEventInfo()
                {
                    EntidadId = null,
                    TipoEvento = ProductoEventInfo.TipoCambioStock,
                    StockActual = d.StockDespuesDeVenta,
                    ProductoNombre = d.Producto.Nombre,
                    ProductoId = d.ProductoId,
                    Descripcion = $"Hubo un cambio en el stock de {d.Producto.Nombre}."
                }).ToList();
            }
            else
            {
                productoEventInfos = venta.DetallesVentasProductos.Select(d => new ProductoEventInfo()
                {
                    EntidadId = venta.Id,
                    TipoEvento = ProductoEventInfo.TipoVenta,
                    StockActual = d.StockDespuesDeVenta,
                    ProductoNombre = d.Producto.Nombre,
                    ProductoId = d.ProductoId,
                    Descripcion = $"Hubo una venta del producto {d.Producto.Nombre}."
                }).ToList();
            }

            productoEventInfos.ForEach(d => _eventPublisher.PublishEvent(d));
        }
    }
}
