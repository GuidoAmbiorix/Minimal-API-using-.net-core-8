using Microsoft.EntityFrameworkCore;
using MinimalAPIPeliculas;

var builder = WebApplication.CreateBuilder(args);
//Inicio de area de los servicios

builder.Services.AddDbContext<ApplicationDbContext>(opciones => opciones.UseSqlServer("name=DefaultConnection"))

builder.Services.AddCors(opciones =>
{
    opciones.AddDefaultPolicy(configuration =>
    {
        configuration.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });

});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//Fin de area de los servicios

var app = builder.Build();


//Inicio de area de los middleware
app.UseSwagger();
app.UseSwaggerUI();
app.UseCors();


app.MapGet("/", () => "Hello World!");


//Fin de area de los middleware
app.Run();
