using GESINV.IdentityHandler;
using GESINV.IdentityHandler.Abstractions;
using GESINV.SubscriptionsService.BackingServices;
using GESINV.SubscriptionsService.BackingServices.Abstractions;
using GESINV.SubscriptionsService.DataAccess;
using GESINV.SubscriptionsService.DataAccess.Interface;
using GESINV.SubscriptionsService.DataAccess.Repositories;
using GESINV.SubscriptionsService.Filters;
using GESINV.SubscriptionsService.Logic;
using GESINV.SubscriptionsService.Logic.Interface;
using GESINV.SubscriptionsService.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddControllers(options => options.Filters.Add(typeof(ExceptionHandlingFilter)));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5300);
});


AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
builder.Services.AddDbContext<SubscriptionsContext>(
    o => o.UseNpgsql(Environment.GetEnvironmentVariable(EnvarionmentVariablesNames.GESINV_SUBSCRIPTIONS_DB_CONNECTION_STRING))
    );


builder.Services.AddScoped<IProductoLogic, ProductoLogic>();
builder.Services.AddScoped<ISuscripcionesStockLogic, SuscripcionesStockLogic>();
builder.Services.AddScoped<ISuscripcionesCompraVentaLogic, SuscripcionesCompraVentaLogic>();
builder.Services.AddScoped<IEventosLogic, EventosLogic>();


builder.Services.AddScoped<IProductosRepository, ProductosRepository>();
builder.Services.AddScoped<ISuscripcionesStockRepository, SuscripcionesStockRepository>();
builder.Services.AddScoped<ISubscriptionsCompraVentaRepository, SubscriptionsCompraVentaRepository>();
builder.Services.AddScoped<IPersistanceConnectionChecker, PersistanceConnectionChecker>();


builder.Services.AddScoped<DbContext, SubscriptionsContext>();
builder.Services.AddScoped<ManejadorDeConfiguracion>();
builder.Services.AddScoped<ITokenHandler, JwtTokenHandler>();
builder.Services.AddScoped<IEmailHandler, HttpEmailHandler>();

builder.Services.AddHttpContextAccessor();


var app = builder.Build();

app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

app.UseSwagger();
app.UseSwaggerUI();

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
