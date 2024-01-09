using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessDDO.DatabaseSettings;
using DataAccessDDO.ModelsDTO;
using Microsoft.EntityFrameworkCore.Update.Internal;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;

namespace DataAccessDDO.Repositories;

public class TagRepo
{
    private readonly DatabaseSettings.DatabaseSettings _databaseSettings;

    public TagRepo(IOptions<DatabaseSettings.DatabaseSettings> databaseSettings)
    {
        _databaseSettings = databaseSettings.Value;
    }

    public TagRepo()
    {

    }

	public int CreateTag(TagDTO dto)
	{
		MySqlConnection connection = new MySqlConnection(_databaseSettings.DefaultConnection);
		connection.Open();

		string query = "INSERT INTO Tag (TagName) VALUES (@TagName); SELECT LAST_INSERT_ID()";

		using MySqlCommand command = new MySqlCommand(query, connection);
		command.Parameters.AddWithValue("@TagName", dto.TagName);

		int newTagId = Convert.ToInt32(command.ExecuteScalar());

		connection.Close();

        Console.WriteLine("New Tag Id " + newTagId);

		return newTagId;
	}

	public void DeleteDreamTags(int dreamId)
	{
		MySqlConnection connection = new MySqlConnection(_databaseSettings.DefaultConnection);
		connection.Open();

		//removes the relation
		string deleteRelationQuery = "DELETE FROM Rest WHERE DreamId = @DreamId";

		using MySqlCommand deleteRelationCommand = new MySqlCommand(deleteRelationQuery, connection);

		deleteRelationCommand.Parameters.AddWithValue("@DreamId", dreamId);

		deleteRelationCommand.ExecuteNonQuery();

		//things not linked in the database are apparently sometimes called orphans
		//remove the orphans. Kill the younglings
		string removeOrphanTagsQuery = "DELETE FROM Tag WHERE NOT EXISTS " +
			"(SELECT 1 FROM Rest WHERE Tag.TagId = Rest.TagId)";

		using MySqlCommand removeOrphanTagsCommand = new MySqlCommand(removeOrphanTagsQuery, connection);

		removeOrphanTagsCommand.ExecuteNonQuery();

		connection.Close();
	}

	public void DeleteTagOrphan()
	{
		MySqlConnection connection = new MySqlConnection(_databaseSettings.DefaultConnection);
		connection.Open();

		string removeOrphanTagsQuery = "DELETE FROM Tag WHERE NOT EXISTS " +
			"(SELECT 1 FROM Rest WHERE Tag.TagId = Rest.TagId)";

		using MySqlCommand removeOrphanTagsCommand = new MySqlCommand(removeOrphanTagsQuery, connection);

		removeOrphanTagsCommand.ExecuteNonQuery();

		connection.Close();
	}

	public void DeleteTagByTagName(string tagName)
	{
		MySqlConnection connection = new MySqlConnection(_databaseSettings.DefaultConnection);
		connection.Open();

		string query = "DELETE FROM Tag WHERE TagName = @tagName";

		using MySqlCommand command = new MySqlCommand(query, connection);

		command.Parameters.AddWithValue("@TagName", tagName);

		command.ExecuteNonQuery();

		connection.Close();
	}

	/*public void DeleteTag(TagDTO dto)
	{
		MySqlConnection connection = new MySqlConnection(_databaseSettings.DefaultConnection);
		connection.Open();

		string query = "DELETE FROM Tag WHERE TagName = @tagName";

		using MySqlCommand command = new MySqlCommand(query, connection);

		command.Parameters.AddWithValue("@TagName", tagName);

		command.ExecuteNonQuery();

		connection.Close();
	}*/
}
