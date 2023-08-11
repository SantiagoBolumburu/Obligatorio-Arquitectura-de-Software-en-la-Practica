using GESINV.AthenticationService.PersistanceAccess;
using GESINV.AthenticationService.PersistanceAccess.Interface;
using GESINV.AthenticationService.PersistanceAccess.Repositories;
using GESINV.AthenticationService.Logic;
using GESINV.AthenticationService.Logic.Interface;
using GESINV.AthenticationService.Utils;
using Microsoft.EntityFrameworkCore;
using System.Net.Security;
using GESINV.AthenticationService.BackingServicesAccess.Abstractions;
using GESINV.AthenticationService.BackingServicesAccess;
using GESINV.IdentityHandler.Abstractions;
using GESINV.IdentityHandler;
using GESINV.AthenticationService.Filters;
using GESINV.AthenticationService.Initializers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddControllers(options => options.Filters.Add(typeof(ExceptionHandlingFilter)));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5100);
});


AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
builder.Services.AddDbContext<AuthenticationContext>(
    o => o.UseNpgsql(Environment.GetEnvironmentVariable(EnvarionmentVariablesNames.GESINV_AUTHENTICATION_DB_CONNECTION_STRING))
    );
builder.Services.AddScoped<DbContext, AuthenticationContext>();

builder.Services.AddScoped<IInvitacionesRepository, InvitacionesRepository>();
builder.Services.AddScoped<IUsuariosRepository, UsuariosRepository>();
builder.Services.AddScoped<IEmpresasRepository, EmpresasRepository>();
builder.Services.AddScoped<IAppkeyRepository, AppkeyRepository>();
builder.Services.AddScoped<IPersistanceConnectionChecker, PersistanceConnectionChecker>();

builder.Services.AddScoped<IInvitacionesLogic, InvitacionesLogic>();
builder.Services.AddScoped<IUsuariosLogic, UsuariosLogic>();
builder.Services.AddScoped<ISessionsLogic, SessionsLogic>();
builder.Services.AddScoped<IAppkeyLogic, AppkeyLogic>();

builder.Services.AddScoped<ManejadorDeConfiguracion>();
builder.Services.AddScoped<ITokenHandler, JwtTokenHandler>(); 
builder.Services.AddScoped<IEmailSender, HttpEmailSender>();
builder.Services.AddSingleton<IKeyValueStorage, DictionaryAccess>();


builder.Services.AddHttpContextAccessor();


var app = builder.Build();

app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

app.UseSwagger();
app.UseSwaggerUI();

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.SeedDB();

app.Run();
