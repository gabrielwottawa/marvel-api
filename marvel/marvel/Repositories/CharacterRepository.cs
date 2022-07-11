using marvel.Database;
using marvel.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace marvel.Repositories
{
    public class CharacterRepository : IRepository<CharacterModel>
    {
        private static Dictionary<int, CharacterModel> characters = new Dictionary<int, CharacterModel>();

        public async Task Add(CharacterModel item)
        {
            await Task.Run(() => insertDB(item));
        }

        public async Task Delete(int id)
        {
            await Task.Run(() => deleteDB(id));
        }

        public async Task Edit(CharacterModel item)
        {
            await Task.Run(() =>
            {
                characters.Remove(item.Id);
                characters.Add(item.Id, item);
            });
        }

        public async Task<CharacterModel> Get(int id)
        {
            return await Task.Run(() => characters.GetValueOrDefault(id));
        }

        public async Task<IEnumerable<CharacterModel>> GetAll()
        {
            return await Task.Run(() => characters.Values.ToList());
        }

        private void insertDB(CharacterModel character)
        {
            var charactersWriter = new CharactersWriter();
            var charactersEntity = new CharactersEntity() { Name = character.Name, DeveloperMarvelId = character.Id };
            charactersWriter.InsertCharacter(charactersEntity);
        }

        private void deleteDB(int id)
        {
            var charactersWriter = new CharactersWriter();
            charactersWriter.DeleteCharacter(id);
        }
    }
}
