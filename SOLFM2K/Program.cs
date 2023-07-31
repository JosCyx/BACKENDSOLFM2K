using Microsoft.EntityFrameworkCore;
using SOLFM2K.Models;

var builder = WebApplication.CreateBuilder(args);
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

var app = builder.Build();


// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors("newPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
