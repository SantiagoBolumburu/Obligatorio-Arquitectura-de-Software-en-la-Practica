using GESINV.ProductsService.Logic;
using GESINV.ProductsService.Logic.Interface;
using GESINV.ProductsService.Utils;

namespace GESINV.ProductsService.Workers
{
    public class RealizadorVentasProgramadasWorker : BackgroundService
    {
        public readonly IServiceProvider Services;

        public RealizadorVentasProgramadasWorker(IServiceProvider services)
        {
            Services = services;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using (var scope = Services.CreateScope())
            {
                var ventasLogic = scope.ServiceProvider.GetRequiredService<IVentasLogic>();
                var manejadorDeConfiguracion = scope.ServiceProvider.GetRequiredService<ManejadorDeConfiguracion>();


                while (!stoppingToken.IsCancellationRequested)
                {
                    int nextRealizacionDelay = manejadorDeConfiguracion.ObtenerIntervaloRealizacionDeVentas();

                    ventasLogic.RealizarVentasProgramadas();

                    await Task.Delay(nextRealizacionDelay, stoppingToken);
                }
            }
        }
    }
}
