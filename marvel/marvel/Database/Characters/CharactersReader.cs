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

        public List<string> SelectCharacter()
        {
            connection.Open();
            var query = "SELECT Name, DeveloperMarvelId FROM Characters";
            var result = connection.Query<string>(query);
            return result.ToList();
        }
    }
}
