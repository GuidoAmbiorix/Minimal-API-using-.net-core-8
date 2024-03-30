using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using MinimalAPIPeliculas;
using MinimalAPIPeliculas.EndPoints;
using MinimalAPIPeliculas.Entidades;
using MinimalAPIPeliculas.Repositorios;

var builder = WebApplication.CreateBuilder(args);
//Inicio de area de los servicios

builder.Services.AddDbContext<ApplicationDbContext>(opciones => opciones.UseSqlServer("name=DefaultConnection"));

builder.Services.AddCors(opciones =>
{
    opciones.AddDefaultPolicy(configuration =>
    {
        configuration.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });

});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped <IRepositorioGeneros, RepositorioGeneros>();
builder.Services.AddAutoMapper(typeof(Program));

//Fin de area de los servicios

var app = builder.Build();


//Inicio de area de los middleware
app.UseSwagger();
app.UseSwaggerUI();
app.UseCors();

var endpointsGeneros = app.MapGroup("/generos").MapGeneros();

//Fin de area de los middleware
app.Run();


