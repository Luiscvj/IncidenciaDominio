using System.Reflection;
using System.Text;
using AspNetCoreRateLimit;
using IncApi.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Persistencia;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
/* ooooooooooooooooluis careverga */

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//Para cambiarl el formato a las respuestas : XML ,etc.
builder.Services.AddControllers(options =>
{
    options.RespectBrowserAcceptHeader = true;
    options.ReturnHttpNotAcceptable = true; //Por si se desea devolver un mensjae que diga que el formato exigido no es aceptado
}).AddXmlSerializerFormatters();


var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Microsoft.AspNetCore.Authorization.AuthorizationMiddleware.Invoke(HttpContext context"));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>{
    opt.TokenValidationParameters = new TokenValidationParameters{
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = key,
        ValidateAudience = false,
        ValidateIssuer = false
    };
});
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
