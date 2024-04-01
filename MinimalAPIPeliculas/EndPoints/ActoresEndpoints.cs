using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MinimalAPIPeliculas.DTOs;
using MinimalAPIPeliculas.Entidades;
using MinimalAPIPeliculas.Migrations;
using MinimalAPIPeliculas.Repositorios;

namespace MinimalAPIPeliculas.EndPoints
{
    public static class ActoresEndpoints
    {
        public static RouteGroupBuilder MapActores(this RouteGroupBuilder group)
        {
            group.MapGet("/", ObtenerActores);
            group.MapGet("/{id:int}", ObtenerPorId);
            group.MapPost("/", CrearActor).DisableAntiforgery();
            group.MapPut("/{id:int}", EditarActor);
            group.MapDelete("/{id:int}", EliminarActor);
            return group;
        }

        static async Task<Ok<List<ActorDTO>>> ObtenerActores(IRepositorioActores repositorio,IMapper mapper)
        {
            var Actores = await repositorio.ObtenerTodos();
            var ActoresDTO = mapper.Map<List<ActorDTO>>(Actores);
            return TypedResults.Ok(ActoresDTO);
        }

        static async Task<Results<Ok<ActorDTO>, NotFound>> ObtenerPorId(IRepositorioActores repositorio, int id,IMapper mapper)
        {
            var Actor = await repositorio.ObtenerPorId(id);

            if (Actor is null)
            {
                return TypedResults.NotFound();
            }

            var ActorDTO = mapper.Map<ActorDTO>(Actor);

            return TypedResults.Ok(ActorDTO);
        }

        static async Task<Created<ActorDTO>> CrearActor([FromForm] CrearActorDTO crearActorDTO, IRepositorioActores repositorio,IMapper mapper)
        {

            var Actor = mapper.Map<Actor>(crearActorDTO);
            var id = await repositorio.Crear(Actor);
            var ActorDTO = mapper.Map<ActorDTO>(Actor);
            return TypedResults.Created($"/Actores/{id}", ActorDTO);
        }

        static async Task<Results<NotFound, NoContent>> EditarActor(int id,[FromForm] CrearActorDTO crearActorDTO, IRepositorioActores repositorio,IMapper mapper)
        {
            var existe = await repositorio.Existe(id);

            if (!existe)
            {
                return TypedResults.NotFound();
            }

            var Actor = mapper.Map<Actor>(crearActorDTO);
            Actor.Id = id;

            await repositorio.Actualizar(Actor);
            return TypedResults.NoContent();
        }

        static async Task<Results<NotFound, NoContent>> EliminarActor(int id, IRepositorioActores repositorio)
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


