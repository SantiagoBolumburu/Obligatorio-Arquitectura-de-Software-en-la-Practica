﻿using GESINV.IdentityHandler.Abstractions;
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
    public class SuscripcionesCompraVentaLogic : ISuscripcionesCompraVentaLogic
    {
        private readonly ITokenHandler _tokenHandler;
        private readonly IProductosRepository _productosRepository;
        private readonly ISubscriptionsCompraVentaRepository _subscriptionsCompraVentaRepository;

        public SuscripcionesCompraVentaLogic(ITokenHandler tokenHandler, IProductosRepository productosRepository, 
            ISubscriptionsCompraVentaRepository subscriptionsCompraVentaRepository)
        {
            _tokenHandler = tokenHandler;
            _productosRepository = productosRepository;
            _subscriptionsCompraVentaRepository = subscriptionsCompraVentaRepository;
        }


        public SubscriptionModelOut Crear(Guid productoId)
        {
            string email = _tokenHandler.GetCurrentSessionInfo().Email;
            Guid usuarioId = _tokenHandler.GetCurrentSessionInfo().UsuarioId;
            Guid empresaIdUsuario = _tokenHandler.GetCurrentSessionInfo().EmpresaId;

            Producto producto = _productosRepository.ObtenerPorMainId(productoId) 
                ?? throw new ArgumentException($"El id: ({productoId}) no se corresponde a ningun producto en el sistema.");

            if (producto.EmpresaId != empresaIdUsuario)
                throw new UnauthorizedAccessException("Usted no puede subscribirse al productos de otras empresas");

            CompraVentaSubscription compraVentaSubscription = new CompraVentaSubscription()
            {
                Id = Guid.NewGuid(),
                Email = email,
                UsuarioId = usuarioId,
                Producto = producto,
                ProductoId = producto.Id
            };

            CompraVentaSubscription subscriptionDb = _subscriptionsCompraVentaRepository.Crear(compraVentaSubscription);

            return DominioAModel.Convertir(subscriptionDb);
        }

        public void Eliminar(Guid productoId)
        {
            Guid usuarioId = _tokenHandler.GetCurrentSessionInfo().UsuarioId;

            CompraVentaSubscription? subscriptionDb = _subscriptionsCompraVentaRepository.ObtenerPorUsuarioIdYProductoId(productoId, usuarioId);

            if (subscriptionDb != null)
                _subscriptionsCompraVentaRepository.Eliminar(subscriptionDb);
        }

        public List<SubscriptionModelOut> ObtenerPorUsuarioEnSession()
        {
            Guid usuarioId = _tokenHandler.GetCurrentSessionInfo().UsuarioId;

            List<CompraVentaSubscription> subscriptions = _subscriptionsCompraVentaRepository.ObtenerPorUsuarioId(usuarioId);

            List<SubscriptionModelOut> subscriptionModels = subscriptions.Select(s => DominioAModel.Convertir(s)).ToList();

            return subscriptionModels;
        }
    }
}
