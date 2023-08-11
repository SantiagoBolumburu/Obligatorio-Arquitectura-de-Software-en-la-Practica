using GESINV.SubscriptionsService.BackingServices.Abstractions;
using GESINV.SubscriptionsService.DataAccess.Interface;
using GESINV.SubscriptionsService.Dominio;
using GESINV.SubscriptionsService.Logic.Interface;
using GESINV.SubscriptionsService.Models;
using GESINV.SubscriptionsService.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GESINV.SubscriptionsService.Logic
{
    public class EventosLogic : IEventosLogic
    {
        private readonly ISubscriptionsCompraVentaRepository _subscriptionsCompraVentaRepository;
        private readonly ISuscripcionesStockRepository _suscripcionesStockRepository;
        private readonly IEmailHandler _emailHandler;
        private readonly ManejadorDeConfiguracion _manejadorDeConfiguracion;

        public EventosLogic(ISubscriptionsCompraVentaRepository subscriptionsCompraVentaRepository,
            ISuscripcionesStockRepository suscripcionesStockRepository, IEmailHandler emailHandler,
            ManejadorDeConfiguracion manejadorDeConfiguracion)
        {
            _subscriptionsCompraVentaRepository = subscriptionsCompraVentaRepository;
            _suscripcionesStockRepository = suscripcionesStockRepository;
            _emailHandler = emailHandler;
            _manejadorDeConfiguracion = manejadorDeConfiguracion;
        }


        public void Agregar(EventoModelIn eventoModelIn)
        {
            int cotaStock = _manejadorDeConfiguracion.ObtenerCotaParaCorreoBajoStock();

            if(eventoModelIn.TipoEvento.Equals(EventoModelIn.TipoVenta))
                NotificarVentas(eventoModelIn);
            if (eventoModelIn.TipoEvento.Equals(EventoModelIn.TipoCompra))
                NotificarCompras(eventoModelIn);
            if (eventoModelIn.StockActual <= cotaStock)
                NotificarBajoStock(eventoModelIn, cotaStock);
        }

        private void NotificarVentas(EventoModelIn eventoModelIn)
        {
            List<CompraVentaSubscription> compraventaSubscriptions =
               _subscriptionsCompraVentaRepository.ObtenerPorProductoMainId(eventoModelIn.ProductoId);

            string newLine = "<br />";

            string fromMail = _manejadorDeConfiguracion.ObtenerDefaultFromEmail() ?? "default@gesinv.com";
            string subject = _manejadorDeConfiguracion.ObtenerDefaultVentaSubject() ?? "Nueva venta de producto";
            string body = $"Nueva venta del producto: {eventoModelIn.ProductoNombre} ({eventoModelIn.ProductoId})." +
                          $"{newLine}El Id de la venta en el sistema es: {eventoModelIn.EntidadId}" +
                          $"{newLine}El stock pasa a ser: {eventoModelIn.StockActual}" +
                          $"{newLine}{eventoModelIn.Descripcion}";

            compraventaSubscriptions.ForEach(s =>
            {
                _emailHandler.EnviarCorreo(fromMail, s.Email, subject, body);
            });
        }

        private void NotificarCompras(EventoModelIn eventoModelIn)
        {
            List<CompraVentaSubscription> compraventaSubscriptions =
                _subscriptionsCompraVentaRepository.ObtenerPorProductoMainId(eventoModelIn.ProductoId);

            string newLine = "<br />";

            string fromMail = _manejadorDeConfiguracion.ObtenerDefaultFromEmail() ?? "default@gesinv.com";
            string subject = _manejadorDeConfiguracion.ObtenerDefaultCompraSubject() ?? "Nueva compra de producto";
            string body = $"Nueva compra del producto: {eventoModelIn.ProductoNombre} ({eventoModelIn.ProductoId})." +
                          $"{newLine}El Id de la compra en el sistema es: {eventoModelIn.EntidadId}" +
                          $"{newLine}El stock pasa a ser: {eventoModelIn.StockActual}" +
                          $"{newLine}{eventoModelIn.Descripcion}";

            compraventaSubscriptions.ForEach(s =>
            {
                _emailHandler.EnviarCorreo(fromMail, s.Email, subject, body);
            });
        }

        private void NotificarBajoStock(EventoModelIn eventoModelIn, int cotaStock)
        {
            List<StockSubscription> stockSubscription =
                _suscripcionesStockRepository.ObtenerPorProductoMainId(eventoModelIn.ProductoId);

            string newLine = "<br />";

            string fromMail = _manejadorDeConfiguracion.ObtenerDefaultFromEmail() ?? "default@gesinv.com";
            string subject = _manejadorDeConfiguracion.ObtenerDefaultStockSubject() ?? "ALERTA: Bajo stock de producto";
            string body = $"El stock del producto: {eventoModelIn.ProductoNombre} ({eventoModelIn.ProductoId}) " +
                          $"esta por debajado de la cota establecida ({cotaStock})." +
                          $"{newLine}Actualmente, el stock es de : {eventoModelIn.StockActual}" +
                          $"{newLine}{eventoModelIn.Descripcion}";

            stockSubscription.ForEach(s =>
            {
                _emailHandler.EnviarCorreo(fromMail, s.Email, subject, body);
            });
        }
    }
}
