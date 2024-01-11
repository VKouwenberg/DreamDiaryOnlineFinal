using DataAccessDDO.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessDDO.DatabaseSettings;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace DataAccessDDO.Repositories;

public class RestRepo : DataAccessInterfaces.IRestRepository
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

	public void DeleteRestByDreamId(int dreamId)
	{
        MySqlConnection connection = new MySqlConnection(_databaseSettings.DefaultConnection);
        connection.Open();

        string query = "DELETE FROM Rest WHERE DreamId = @DreamId";

        using MySqlCommand command = new MySqlCommand(query, connection);
        command.Parameters.AddWithValue("@DreamId", dreamId);

        command.ExecuteNonQuery();

        connection.Close();
    }

    public void DeleteRestByTagIdAndDreamId(int tagId, int dreamId)
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
