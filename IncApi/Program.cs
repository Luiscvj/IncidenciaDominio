using System.Reflection;
using AspNetCoreRateLimit;
using IncApi.Extensions;
using Microsoft.EntityFrameworkCore;
using Persistencia;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//Para cambiarl el formato a las respuestas : XML ,etc.
builder.Services.AddControllers(options =>
{
    options.RespectBrowserAcceptHeader = true;
    options.ReturnHttpNotAcceptable = true; //Por si se desea devolver un mensjae que diga que el formato exigido no es aceptado
}).AddXmlSerializerFormatters();

builder.Services.AddDbContext<IncidenciaContext>(Options =>
{
   string ? ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    Options.UseMySql(ConnectionString,ServerVersion.AutoDetect(ConnectionString));
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureRateLimiting();
builder.Services.ConfigureApiVersioning();
builder.Services.AddAplicationServices();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddCors(); 

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseIpRateLimiting();

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
