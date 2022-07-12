using Microsoft.Data.SqlClient;
using Dapper;

namespace marvel.Database
{
    public class CharactersWriter
    {
        private SqlConnection connection;

        public CharactersWriter()
        {
            var dbConnection = new DbConnection();
            connection = dbConnection.GetConnection();
        }

        public void InsertCharacter(CharactersEntity charactersEntity)
        {
            connection.Open();
            var query = "INSERT INTO FavoritesCharacters (Name, DeveloperMarvelId) VALUES (@Name, @DeveloperMarvelId)";
            var param = new DynamicParameters();
            param.Add("@Name", charactersEntity.Name);
            param.Add("@DeveloperMarvelId", charactersEntity.DeveloperMarvelId);
            connection.Execute(query, param);
        }

        public void DeleteCharacter(int id)
        {
            connection.Open();
            var query = "DELETE FavoritesCharacters WHERE Id = @Id";
            var param = new DynamicParameters();
            param.Add("@Id", id);
            connection.Execute(query, param);
        }
    }
}
