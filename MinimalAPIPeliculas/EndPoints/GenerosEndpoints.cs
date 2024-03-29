using Microsoft.AspNetCore.Http.HttpResults;
using MinimalAPIPeliculas.Entidades;
using MinimalAPIPeliculas.Repositorios;

namespace MinimalAPIPeliculas.EndPoints
{
    public static class GenerosEndpoints
    {
        public static RouteGroupBuilder MapGeneros(this RouteGroupBuilder group)
        {
            group.MapGet("/", ObtenerGeneros);
            group.MapGet("/{id:int}", ObtenerPorId);
            group.MapPost("/", CrearGenero);
            group.MapPut("/{id:int}", EditarGenero);
            group.MapDelete("/{id:int}", EliminarGenero);
            return group;
        }

        static async Task<Ok<List<Genero>>> ObtenerGeneros(IRepositorioGeneros repositorio)
        {
            var generos = await repositorio.ObtenerTodos();
            return TypedResults.Ok(generos);
        }

        static async Task<Results<Ok<Genero>, NotFound>> ObtenerPorId(IRepositorioGeneros repositorio, int id)
        {
            var genero = await repositorio.ObtenerPorId(id);

            if (genero is null)
            {
                return TypedResults.NotFound();
            }

            return TypedResults.Ok(genero);
        }

        static async Task<Created<Genero>> CrearGenero(Genero genero, IRepositorioGeneros repositorio)
        {
            var id = await repositorio.Crear(genero);
            return TypedResults.Created($"/generos/{id}", genero);
        }

        static async Task<Results<NotFound, NoContent>> EditarGenero(int id, Genero genero, IRepositorioGeneros repositorio)
        {
            var existe = await repositorio.Existe(id);

            if (!existe)
            {
                return TypedResults.NotFound();
            }

            await repositorio.Actualizar(genero);
            return TypedResults.NoContent();
        }

        static async Task<Results<NotFound, NoContent>> EliminarGenero(int id, IRepositorioGeneros repositorio)
        {
            var existe = await repositorio.Existe(id);

            if (!existe)
            {
                return TypedResults.NotFound();
            }

            await repositorio.Borrar(id);
            return TypedResults.NoContent();

        }
    }

}


