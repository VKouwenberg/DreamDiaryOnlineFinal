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

public class TagRepo : DataAccessInterfaces.ITagRepository
{
    private readonly DatabaseSettings.DatabaseSettings _databaseSettings;
    private readonly RestRepo _restRepo;

    public TagRepo(IOptions<DatabaseSettings.DatabaseSettings> databaseSettings, RestRepo restRepo)
    {
        _databaseSettings = databaseSettings.Value;
        _restRepo = restRepo;
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

        _restRepo.DeleteRestByDreamId(dreamId);

        //things not linked in the database are apparently sometimes called orphans
        //remove the orphans. Kill the younglings
        string removeOrphanTagsQuery = "DELETE FROM Tag WHERE NOT EXISTS " +
            "(SELECT 1 FROM Rest WHERE Tag.TagId = Rest.TagId)";

        using MySqlCommand removeOrphanTagsCommand = new MySqlCommand(removeOrphanTagsQuery, connection);

        removeOrphanTagsCommand.ExecuteNonQuery();

        connection.Close();
    }

    public void DeleteTagsByDreamId(int dreamId)
    {
        MySqlConnection connection = new MySqlConnection(_databaseSettings.DefaultConnection);
        connection.Open();

        string deleteTagsQuery = "DELETE FROM Tag WHERE TagId IN " +
                             "(SELECT DISTINCT TagId FROM Rest WHERE DreamId = @DreamId)";

        using MySqlCommand deleteTagsCommand = new MySqlCommand(deleteTagsQuery, connection);
        deleteTagsCommand.Parameters.AddWithValue("@DreamId", dreamId);

        deleteTagsCommand.ExecuteNonQuery();

        connection.Close();
    }
}
