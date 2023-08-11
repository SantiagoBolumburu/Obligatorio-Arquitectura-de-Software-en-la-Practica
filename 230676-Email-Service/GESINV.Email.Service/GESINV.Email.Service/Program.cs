using GESINV.Email.Service.Logic;
using GESINV.Email.Service.Logic.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IEmailHandler, EmailHandler>();

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5400);
});

//builder.WebHost.UseUrls(";http://[::]:5400;");
//builder.WebHost.UseUrls(";http://[::]:5400;https://[::]:5401");


var app = builder.Build();

app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
