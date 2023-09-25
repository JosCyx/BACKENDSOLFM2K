using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SOLFM2K.Models;
using SOLFM2K.Services;
using SOLFM2K.Services.CryptoService;
using SOLFM2K.Services.EmailService;
using System.Text;


//var  secretKey = "RQ9aP&1BvXxZ$uFqGKsX5GmDwN8@Y3T!";
var builder = WebApplication.CreateBuilder(args);

//coregir acceso a configuracion de jwt
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var issuer = jwtSettings["Issuer"];
var audience = jwtSettings["Audience"];
var secretKey = jwtSettings["SecretKey"];

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//add context
builder.Services.AddDbContext<SolicitudContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("conn"));
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

//Registra el servicio de encriptación
builder.Services.AddSingleton(provider => "eb&zgVadt%Xis2T2");
builder.Services.AddScoped<ICryptoService, CryptoService>();

// Registra el servicio ITokenService
builder.Services.AddScoped<ITokenService, TokenService>();

//Registra el servicio IEmailService
builder.Services.AddScoped<IEmailService, EmailService>();

// Registrar el filtro de autorización personalizado
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
