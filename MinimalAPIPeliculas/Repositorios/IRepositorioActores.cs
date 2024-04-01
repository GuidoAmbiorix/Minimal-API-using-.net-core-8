using MinimalAPIPeliculas.Entidades;

namespace MinimalAPIPeliculas.Repositorios
{
    public interface IRepositorioActores
    {
        Task<List<Actor>> ObtenerTodos();

        Task<Actor?> ObtenerPorId(int id);

        Task<int> Crear(Actor genero);

        Task<bool> Existe(int id);

        Task Actualizar(Actor genero);

        Task Borrar(int id);
    }
}
