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
    
    public void CreateTag(TagDTO dto)
    {
        MySqlConnection connection = new MySqlConnection(_databaseSettings.DefaultConnection);
        connection.Open();

        string query = "INSERT INTO Tag (TagName, RestId) VALUES (@TagName, @RestId)";

        using MySqlCommand command = new MySqlCommand(query, connection);
        command.Parameters.AddWithValue("@TagName", dto.TagName);
        command.Parameters.AddWithValue("@RestId", dto.RestId);

        command.ExecuteNonQuery();
        connection.Close();
    }

    //deletes all tags associated with a dreamId
    public void DeleteDreamTags (int dreamId)
    {
        MySqlConnection connection = new MySqlConnection(_databaseSettings.DefaultConnection);
        connection.Open();

        string query = "DELETE FROM Tag WHERE DreamId = @DreamId";

        using MySqlCommand command = new MySqlCommand(query, connection);

        command.Parameters.AddWithValue("@DreamId", dreamId);

        command.ExecuteNonQuery();

        connection.Close();
    }

}
