using NetCoreToSqlOraclePersonajes.Models;

namespace NetCoreToSqlOraclePersonajes.Repositories
{
    public interface IRepositoryPersonajes
    {
        List<Personaje> GetPersonajes();

        void InsertPersonajes(int id, string nombre, string imagen);

        Personaje FindPersonaje(int id);

        void UpdatePersonaje(int id, string nombre, string imagen);

        void DeletePersonaje(int id);
    }
}
