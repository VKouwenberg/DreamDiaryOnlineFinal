using DataAccessDDO.DatabaseSettings;
using DataAccessDDO.ModelsDTO;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using System;
using System.Linq;
using System.Collections.Generic;

namespace DataAccessDDO.Repositories;

public class DreamRepo
{
    private readonly DatabaseSettings.DatabaseSettings _databaseSettings;

    public DreamRepo(IOptions<DatabaseSettings.DatabaseSettings> databaseSettings)
    {
        _databaseSettings = databaseSettings.Value;
    }



    public void CreateDream(DreamDTO dto)
    {
        using (MySqlConnection connection = new MySqlConnection(_databaseSettings.DefaultConnection))
        {
            connection.Open();

            string query = "INSERT INTO Dream (DreamName, DreamText, DreamerId) VALUES (@DreamName, @DreamText, @ReadableBy, @DreamerId)";

            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@DreamName", dto.DreamName);
                command.Parameters.AddWithValue("@DreamText", dto.DreamText);
                command.Parameters.AddWithValue("@ReadableBy", dto.ReadableBy);

                command.ExecuteNonQuery();
            }
        }
    }



    public void UpdateDream(DreamDTO dto)
    {
        using (MySqlConnection connection = new MySqlConnection(_databaseSettings.DefaultConnection))
        {
            connection.Open();
            string query = "UPDATE Dream SET DreamName = @DreamName, DreamText = @DreamText WHERE DreamId = @DreamId";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@DreamId", dto.DreamId);
                command.Parameters.AddWithValue("@DreamName", dto.DreamName);
                command.Parameters.AddWithValue("@DreamText", dto.DreamText);
                command.Parameters.AddWithValue("@ReadableBy", dto.ReadableBy);
                command.ExecuteNonQuery();
            }
        }
    }

    public void DeleteDream(int dreamId)
    {
        using (MySqlConnection connection = new MySqlConnection(_databaseSettings.DefaultConnection))
        {
            connection.Open();
            string query = "DELETE FROM Dream WHERE DreamId = @DreamId";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@DreamId", dreamId);
                command.ExecuteNonQuery();
            }
        }
    }

    

    public List<DreamDTO> GetAllDreams()
    {
        Console.WriteLine("This is the string in DreamRepo");
        Console.WriteLine($"Connection String: {_databaseSettings.DefaultConnection}");


        List<DreamDTO> dreams = new List<DreamDTO>();

        MySqlConnection connection = new MySqlConnection(_databaseSettings.DefaultConnection);
        connection.Open();

        string query = @"SELECT " +
            "dream.DreamId, " +
            "dream.DreamName, " +
            "dream.DreamText, " +
            "dream.ReadableBy, " +
            "tag.TagId, " +
            "tag.TagName " +
            "FROM dream " +
            "LEFT JOIN rest ON dream.DreamId = rest.DreamId " +
            "LEFT JOIN tag ON rest.TagId = tag.TagId";

        MySqlCommand command = new MySqlCommand(query, connection);
        MySqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            DreamDTO dto = dreams.FirstOrDefault(d => d.DreamId == Convert.ToInt32(reader["DreamId"]));

            if (dto == null)
            {
                dto = new DreamDTO
                {
                    DreamId = Convert.ToInt32(reader["DreamId"]),
                    DreamName = reader["DreamName"].ToString(),
                    DreamText = reader["DreamText"].ToString(),
                    ReadableBy = reader["ReadableBy"].ToString(),
                    Tags = new List<TagDTO>()
                };

                dreams.Add(dto);
            }

            if (reader["TagId"] != DBNull.Value)
            {
                dto.Tags.Add(new TagDTO
                {
                    TagId = Convert.ToInt32(reader["TagId"]),
                    TagName = reader["TagName"].ToString()
                });
            }
        }
        connection.Close();

        return dreams;
    }

    public DreamDTO GetDream(int dreamId)
    {
			DreamDTO dto = null;

        MySqlConnection connection = new MySqlConnection(_databaseSettings.DefaultConnection);
        connection.Open();

        string query = @"SELECT "+
            "dream.DreamId, " +
            "dream.DreamName, " +
            "dream.DreamText, " +
            "dream.ReadableBy, " +
				"tag.TagId, " +
				"tag.TagName " +
				"FROM dream " +
				"JOIN rest ON dream.DreamId = rest.DreamId " +
				"JOIN tag ON rest.TagId = tag.TagId " +
				"WHERE dream.DreamId = @DreamId";

        MySqlCommand command = new MySqlCommand(query, connection);
			command.Parameters.AddWithValue("@DreamId", dreamId);
        MySqlDataReader reader = command.ExecuteReader();

			while (reader.Read())
			{
				if (dto == null)
				{
                dto = new DreamDTO
                {
                    DreamId = Convert.ToInt32(reader["DreamId"]),
                    DreamName = reader["DreamName"].ToString(),
                    DreamText = reader["DreamText"].ToString(),
                    ReadableBy = reader["ReadableBy"].ToString(),
                    Tags = new List<TagDTO>()
                };
            }

            if (reader["TagId"] != DBNull.Value)
            {
                dto.Tags.Add(new TagDTO
                {
                    TagId = Convert.ToInt32(reader["TagId"]),
                    TagName = reader["TagName"].ToString()
                });
            }


        }
			connection.Close();

			return dto;
    }

    /*public List<DreamDTO> GetAllDreams()
    {
        Console.WriteLine("This is the string in DreamRepo");
        Console.WriteLine($"Connection String: {_databaseSettings.DefaultConnection}");

        List<DreamDTO> dreams = new List<DreamDTO>();

        MySqlConnection connection = new MySqlConnection(_databaseSettings.DefaultConnection);
        connection.Open();

        string query = "SELECT * FROM Dream";

        MySqlCommand command = new MySqlCommand(query, connection);
        MySqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            DreamDTO dto = new DreamDTO()
            {
                DreamId = Convert.ToInt32(reader["DreamId"]),
                DreamName = reader["DreamName"].ToString(),
                DreamText = reader["DreamText"].ToString(),
                ReadableBy = reader["ReadableBy"].ToString()
            };

            dreams.Add(dto);
        }

			connection.Close();
        return dreams;
    }*/


    /*public void CreateDreamForDreamer(DreamDTO dream, DreamerDTO dreamer)
		{
			using (MySqlConnection connection = new MySqlConnection(_databaseSettings.DefaultConnection))
			{
				connection.Open();

				string query = "INSERT INTO Dream (DreamName, DreamText, DreamerId) VALUES (@DreamName, @DreamText, @ReadableBy, @DreamerId)";

				using (MySqlCommand command = new MySqlCommand(query, connection))
				{
					command.Parameters.AddWithValue("@DreamName", dream.DreamName);
					command.Parameters.AddWithValue("@DreamText", dream.DreamText);
					command.Parameters.AddWithValue("@ReadableBy", dream.ReadableBy);

					command.ExecuteNonQuery();
				}
			}
		}*/

    /*public List<DreamDTO> GetAllDreamsByDreamerId(int dreamerId)
		{
			List<DreamDTO> dreams = new List<DreamDTO>();

			using (MySqlConnection connection = new MySqlConnection(_databaseSettings.DefaultConnection))
			{
				connection.Open();

				string query = "SELECT * FROM Dream WHERE DreamerId = @DreamerId";

				using (MySqlCommand command = new MySqlCommand(query, connection))
				{
					command.Parameters.AddWithValue("@DreamerId", dreamerId);

					using (MySqlDataReader reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							DreamDTO dto = new DreamDTO
							{
								DreamId = Convert.ToInt32(reader["DreamId"]),
								DreamName = reader["DreamName"].ToString(),
								DreamText = reader["DreamText"].ToString(),
								ReadableBy = reader["ReadableBy"].ToString()
							};

							dreams.Add(dto);
						}
					}
				}
			}

			return dreams;
		}*/

    /*public List<DreamDTO> GetDreamsByDreamerId(int dreamerId)
		{
			List<DreamDTO> dreams = new List<DreamDTO>();

			using MySqlConnection connection = new MySqlConnection(_databaseSettings.DefaultConnection);
			connection.Open();

			string query = "SELECT * FROM Dream WHERE DreamerId = @DreamerId";

			using MySqlCommand command = new MySqlCommand(query, connection);
			command.Parameters.AddWithValue("@DreamerId", dreamerId);

			using MySqlDataReader reader = command.ExecuteReader();

			while (reader.Read())
			{
				DreamDTO dream = new DreamDTO
				{
					DreamId = Convert.ToInt32(reader["DreamId"]),
					DreamName = reader["DreamName"].ToString(),
					DreamText = reader["DreamText"].ToString(),
					ReadableBy = reader["ReadableBy"].ToString()
				};

				dreams.Add(dream);
			}
			connection.Close();

			return dreams;
		}*/
    /*public DreamDTO ReadDream(int dreamId)
		{
			using (MySqlConnection connection = new MySqlConnection(_databaseSettings.DefaultConnection))
			{
				connection.Open();
				string query = "SELECT * FROM Dream WHERE DreamId = @DreamId";
				using (MySqlCommand command = new MySqlCommand(query, connection))
				{
					command.Parameters.AddWithValue("@DreamId", dreamId);
					using (MySqlDataReader reader = command.ExecuteReader())
					{
						if (reader.Read())
						{
							return new DreamDTO
							{
								DreamId = Convert.ToInt32(reader["DreamId"]),
								DreamName = reader["DreamName"].ToString(),
								DreamText = reader["DreamText"].ToString(),
								ReadableBy = reader["ReadableBy"].ToString()
							};
						}
					}
				}
			}
			return null;
		}*/
}
