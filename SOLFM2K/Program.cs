using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SOLFM2K.Models;
using SOLFM2K.Services;
using SOLFM2K.Services.CryptoService;
using SOLFM2K.Services.EmailService;
using SOLFM2K.Services.ExtractService;
using SOLFM2K.Services.WDAuthenticate;
using System.Text;


var builder = WebApplication.CreateBuilder(args);
var bean = builder.Configuration.GetSection("Bean");

//coregir acceso a configuracion de jwt
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var issuer = jwtSettings["Issuer"];
var audience = jwtSettings["Audience"];
var secretKey = jwtSettings["SecretKey"];
//var secretKey = Environment.GetEnvironmentVariable("SECRET_KEY");

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//extraer cadena de conexion de las variables de entorno
//var conn = Environment.GetEnvironmentVariable("DB_CONN");

//add context
builder.Services.AddDbContext<SolicitudContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("conn"));
    //options.UseSqlServer(conn);
});

//add policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("newPolicy",
        app =>
        {
            app.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
        });
});


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidIssuer = issuer,
            ValidAudience = audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
        };
    });


//Registra el servicio ExtractService
builder.Services.AddScoped<IExtractService, ExtractService>();

//crea una instancia del servicio ExtracService para registrar el CryptoService con la palabra clave
using (var scope = builder.Services.BuildServiceProvider().CreateScope())
{
    
    var beanService = scope.ServiceProvider.GetRequiredService<IExtractService>();
    string masterKey = beanService.ExtractBean();

    // Registra CryptoService con la palabra clave extra�da
    builder.Services.AddSingleton(provider => masterKey);
    builder.Services.AddScoped<ICryptoService, CryptoService>();

}

// Registra el servicio ITokenService
builder.Services.AddScoped<IAuthorizeService, AuthorizeService>();

// Registra el servicio ITokenService
builder.Services.AddScoped<ITokenService, TokenService>();

//Registra el servicio IEmailService
builder.Services.AddScoped<IEmailService, EmailService>();

// Registrar el filtro de autorizaci�n personalizado
builder.Services.AddScoped<JwtAuthorizationFilter>();


//contruye la app
var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors("newPolicy");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
