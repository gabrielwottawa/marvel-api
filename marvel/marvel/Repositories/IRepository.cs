using System.Collections.Generic;
using System.Threading.Tasks;

namespace marvel.Repositories
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAllCharacters(int orderBy);
        Task<IEnumerable<T>> GetCharactersByName(string name);
        Task<IEnumerable<T>> GetFavoritesCharacters();
        Task Add(T item);
        Task Delete(int id);
    }
}
