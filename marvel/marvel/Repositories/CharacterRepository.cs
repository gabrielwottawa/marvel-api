using marvel.Database;
using marvel.Models;
using marvel.Request;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace marvel.Repositories
{
    public class CharacterRepository : IRepository<CharacterModel>
    {
        public async Task<IEnumerable<CharacterModel>> GetCharactersByName(string name)
        {
            var apiMarvel = new RequestApiMarvel();
            return await Task.Run(() => apiMarvel.GetCharactersByName(name));
        }

        public async Task<IEnumerable<CharacterModel>> GetAllCharacters(int orderBy)
        {
            var apiMarvel = new RequestApiMarvel();
            return await Task.Run(() => apiMarvel.GetAllCharacters(orderBy));
        }

        public async Task<IEnumerable<CharacterModel>> GetFavoritesCharacters()
        {
            var requestFavoritesCharacters = new RequestFavoritesCharacters();
            return await Task.Run(() => requestFavoritesCharacters.GetAllFavoritesCharacters());
        }

        public async Task Add(CharacterModel item)
        {
            var charactersWriter = new CharactersWriter();
            var charactersEntity = new CharactersEntity() { Name = item.Name, DeveloperMarvelId = item.Id };
            await Task.Run(() => charactersWriter.InsertCharacter(charactersEntity));
        }

        public async Task Delete(int id)
        {
            var charactersWriter = new CharactersWriter();
            await Task.Run(() => charactersWriter.DeleteCharacter(id));
        }
    }
}
