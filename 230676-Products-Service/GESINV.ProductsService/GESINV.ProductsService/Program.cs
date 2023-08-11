using GESINV.IdentityHandler.Abstractions;
using GESINV.IdentityHandler;
using GESINV.ProductsService.Utils;
using Microsoft.EntityFrameworkCore;
using GESINV.ProductsService.PersistanceAccess;
using GESINV.ProductsService.PersistanceAccess.Interface;
using GESINV.ProductsService.PersistanceAccess.Repositories;
using GESINV.ProductsService.Logic.Interface;
using GESINV.ProductsService.Logic;
using GESINV.ProductsService.BackingServices.Interface;
using GESINV.ProductsService.BackingServices;
using GESINV.ProductsService.Filters;
using GESINV.ProductsService.Workers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddControllers(options => options.Filters.Add(typeof(ExceptionHandlingFilter)));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5200);
});

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
builder.Services.AddDbContext<ProductsContext>(
    o => o.UseNpgsql(Environment.GetEnvironmentVariable(EnvarionmentVariablesNames.GESINV_PRODUCTS_DB_CONNECTION_STRING))
    );

builder.Services.AddScoped<IProductosLogic, ProductosLogic>();
builder.Services.AddScoped<IProveedoresLogic, ProveedoresLogic>();
builder.Services.AddScoped<IComprasLogic, ComprasLogic>();
builder.Services.AddScoped<IVentasLogic, VentasLogic>();
builder.Services.AddScoped<IReportesLogic, ReportesLogic>();

builder.Services.AddScoped<IProductosRepository, ProductosRepository>();
builder.Services.AddScoped<IProveedoresRepository, ProveedoresRepository>();
builder.Services.AddScoped<IComprasRepository, ComprasRepository>();
builder.Services.AddScoped<IVentasRepository, VentasRepository>();
builder.Services.AddScoped<IPersistanceConnectionChecker, PersistanceConnectionChecker>();

builder.Services.AddScoped<DbContext, ProductsContext>();
builder.Services.AddScoped<ManejadorDeConfiguracion>();
builder.Services.AddScoped<ITokenHandler, JwtTokenHandler>();
builder.Services.AddScoped<IEventPublisher, HttpEventPublisher>();
builder.Services.AddScoped<IEmailHandler, HttpEmailHandler>();


builder.Services.AddHostedService<RealizadorVentasProgramadasWorker>();


builder.Services.AddHttpContextAccessor();


var app = builder.Build();

app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());


app.UseSwagger();
app.UseSwaggerUI();

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();