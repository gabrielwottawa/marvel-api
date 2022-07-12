using marvel.Database.Characters;
using marvel.Models;
using System.Collections.Generic;
using System.Linq;

namespace marvel.Request
{
    public class RequestFavoritesCharacters
    {
        private CharactersReader charactersReader;
        private RequestApiMarvel requestApi;

        public RequestFavoritesCharacters()
        {
            charactersReader = new CharactersReader();
            requestApi = new RequestApiMarvel();
        }

        public IEnumerable<CharacterModel> GetAllFavoritesCharacters()
        {
            var favorites = charactersReader.SelectCharacter();
            var characterModelList = new List<CharacterModel>();

            foreach (var fv in favorites)
                characterModelList.Add(requestApi.GetCharactersByName(fv.Name).Where(el => el.Id == fv.DeveloperMarvelId).FirstOrDefault());

            return characterModelList;
        }
    }
}
