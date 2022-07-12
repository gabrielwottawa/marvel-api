using Dapper;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;

namespace marvel.Database.Characters
{
    public class CharactersReader
    {
        private SqlConnection connection;

        public CharactersReader()
        {
            var dbConnection = new DbConnection();
            connection = dbConnection.GetConnection();
        }

        public List<CharactersEntity> SelectCharacter()
        {
            connection.Open();
            var query = "SELECT Name, DeveloperMarvelId FROM FavoritesCharacters";
            var result = connection.Query<CharactersEntity>(query);
            return result.ToList();
        }
    }
}
