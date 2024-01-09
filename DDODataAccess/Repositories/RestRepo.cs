using DataAccessDDO.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessDDO.DatabaseSettings;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;

namespace DataAccessDDO.Repositories;

public class RestRepo
{
    private readonly DatabaseSettings.DatabaseSettings _databaseSettings;

    public RestRepo(IOptions<DatabaseSettings.DatabaseSettings> databaseSettings)
    {
        _databaseSettings = databaseSettings.Value;
    }

    public void CreateRest(int tagId, int dreamId)
    {
		using MySqlConnection connection = new MySqlConnection(_databaseSettings.DefaultConnection);
		connection.Open();

        string query = "INSERT INTO Rest (TagId, DreamId) VALUES (@TagId, @DreamId)";

        using MySqlCommand command = new MySqlCommand(query, connection);
        command.Parameters.AddWithValue("@TagId", tagId);
		command.Parameters.AddWithValue("@DreamId", dreamId);

        command.ExecuteNonQuery();

		connection.Close();
	}

    public void DeleteRest(int tagId, int dreamId)
    {
		MySqlConnection connection = new MySqlConnection(_databaseSettings.DefaultConnection);
		connection.Open();

		string query = "DELETE FROM Rest WHERE TagId = @TagId AND DreamId = @DreamId";

		using MySqlCommand command = new MySqlCommand(query, connection);
		command.Parameters.AddWithValue("@TagId", tagId);
		command.Parameters.AddWithValue("@DreamId", dreamId);

		command.ExecuteNonQuery();

		connection.Close();
	}
}
