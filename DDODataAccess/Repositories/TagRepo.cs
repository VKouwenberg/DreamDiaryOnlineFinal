using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessDDO.DatabaseSettings;
using DataAccessDDO.ModelsDTO;
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

    public void CreateTag(TagDTO tag)
    {
        MySqlConnection connection = new MySqlConnection(_databaseSettings.DefaultConnection);
        connection.Open();

        string query = "INSERT INTO Tag (TagName) VALUES (@TagName)";

        using (MySqlCommand command = new MySqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@TagName", tag.TagName);

            command.ExecuteNonQuery();
        }

        connection.Close();
    }
}
