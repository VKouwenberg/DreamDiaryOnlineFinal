using DataAccessDDO.DatabaseSettings;
using DataAccessDDO.ModelsDTO;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using System;
using System.Linq;
using System.Collections.Generic;
using DataAccessDDO.Repositories;
using Azure;
using System.IO;
using Org.BouncyCastle.Utilities.IO;

namespace DataAccessDDO.Repositories;

public class DreamRepo
{
    private readonly DatabaseSettings.DatabaseSettings _databaseSettings;
    private readonly TagRepo _tagRepo;
    private readonly RestRepo _restRepo;

    public DreamRepo(IOptions<DatabaseSettings.DatabaseSettings> databaseSettings,
        TagRepo tagRepo,
        RestRepo restRepo)
    {
        _databaseSettings = databaseSettings.Value;
        _tagRepo = tagRepo;
        _restRepo = restRepo;
    }

    public List<DreamDTO> GetAllDreams()
    {
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

    public void CreateDream(DreamDTO dto)
    {
        using MySqlConnection connection = new MySqlConnection(_databaseSettings.DefaultConnection);

        connection.Open();

        string insertQuery = "INSERT INTO Dream (DreamName, DreamText, ReadableBy) VALUES (@DreamName, @DreamText, @ReadableBy)";

        using MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection);

        insertCommand.Parameters.AddWithValue("@DreamName", dto.DreamName);
        insertCommand.Parameters.AddWithValue("@DreamText", dto.DreamText);
        insertCommand.Parameters.AddWithValue("@ReadableBy", dto.ReadableBy);

        insertCommand.ExecuteNonQuery();

        //get the last inserted dreamId
        string lastInsertIdQuery = "SELECT LAST_INSERT_ID()";

        using (MySqlCommand lastInsertIdCommand = new MySqlCommand(lastInsertIdQuery, connection))
        {
            dto.DreamId = Convert.ToInt32(lastInsertIdCommand.ExecuteScalar());
        }

        if (dto.Tags != null && dto.Tags.Any())
        {
            foreach (TagDTO tag in dto.Tags)
            {
                //check if tag already exists
                string tagExistQuery = "SELECT TagId FROM Tag WHERE TagName = @TagName";

                using MySqlCommand tagExistsCommand = new MySqlCommand(tagExistQuery, connection);

                tagExistsCommand.Parameters.AddWithValue("@TagName", tag.TagName);
                object existingTagIdObject = tagExistsCommand.ExecuteScalar();


                if (existingTagIdObject != null)
                {
                    //tag exists, link current tag
                    tag.TagId = (int)existingTagIdObject;
                }
                else
                {
                    //adds the tag to the database
                    string tagQuery = "INSERT INTO Tag (TagName) VALUES(@TagName)";

                    using MySqlCommand tagCommand = new MySqlCommand(tagQuery, connection);

                    tagCommand.Parameters.AddWithValue("@TagName", tag.TagName);

                    tagCommand.ExecuteNonQuery();

                    //get the last inserted tagId
                    string lastTagInsertIdQuery = "SELECT LAST_INSERT_ID()";
                    using MySqlCommand lastTagInsertIdCommand = new MySqlCommand(lastTagInsertIdQuery, connection);

                    tag.TagId = Convert.ToInt32(lastTagInsertIdCommand.ExecuteScalar());
                }

                //insert into rest table
                string restQuery = "INSERT INTO Rest (DreamId, TagId) VALUES (@DreamId, @TagId)";
                using MySqlCommand restCommand = new MySqlCommand(restQuery, connection);

                restCommand.Parameters.AddWithValue("@DreamId", dto.DreamId);
                restCommand.Parameters.AddWithValue("@TagId", tag.TagId);

                restCommand.ExecuteNonQuery();
            }
        }

        connection.Close();
    }

    public void UpdateDream(DreamDTO dto)
    {
        Console.WriteLine("DTO RECEIVED TO UPDATE");
        Console.WriteLine(dto.DreamName);
		Console.WriteLine(dto.DreamText);
        if (dto.Tags != null && dto.Tags.Any()) 
        {
            foreach (TagDTO tag in dto.Tags)
            {
                Console.WriteLine(tag.TagName);
            }
        }
        Console.WriteLine("END OF DREAM");


		using MySqlConnection connection = new MySqlConnection(_databaseSettings.DefaultConnection);

        connection.Open();

        //update the dream
        string updateDreamQuery = "UPDATE Dream SET " +
            "DreamName = @DreamName, " +
            "DreamText = @DreamText, " +
            "ReadableBy = @ReadableBy " +
            "WHERE DreamId = @DreamId";

        using MySqlCommand UpdateDreamCommand = new MySqlCommand(updateDreamQuery, connection);

        UpdateDreamCommand.Parameters.AddWithValue("@DreamName", dto.DreamName);
        UpdateDreamCommand.Parameters.AddWithValue("@DreamText", dto.DreamText);
        UpdateDreamCommand.Parameters.AddWithValue("@ReadableBy", dto.ReadableBy);
        UpdateDreamCommand.Parameters.AddWithValue("@DreamId", dto.DreamId);

        UpdateDreamCommand.ExecuteNonQuery();

        //removes all existing tags with this id
        _tagRepo.DeleteDreamTags(dto.DreamId);


        //add new tags
        if (dto.Tags != null && dto.Tags.Any())
        {
            foreach (TagDTO tag in dto.Tags)
            {
				Console.WriteLine("TAG ID DREAM ID IN ORDER BEFORE CREATING THE TAG");
				Console.WriteLine(tag.TagId);
				Console.WriteLine(dto.DreamId);

                if (!string.IsNullOrWhiteSpace(tag.TagName))
                {
					//create the tag itself
					int newTagId = _tagRepo.CreateTag(tag);

					Console.WriteLine("TAG ID DREAM ID NEW ID IN ORDER AFTER CREATING THE TAG");
					Console.WriteLine(tag.TagId);
					Console.WriteLine(dto.DreamId);
					Console.WriteLine(newTagId);

					//link the tag and the dreamId in a new rest
					_restRepo.CreateRest(newTagId, dto.DreamId);
				}
                else
                {
                    _restRepo.DeleteRest(tag.TagId, dto.DreamId);
                }
            }
        }

        connection.Close();
    }

    public void DeleteDream(int dreamId)
    {
        using MySqlConnection connection = new MySqlConnection(_databaseSettings.DefaultConnection);

        connection.Open();
        string query = "DELETE FROM Dream WHERE DreamId = @DreamId";
        using (MySqlCommand command = new MySqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@DreamId", dreamId);
            command.ExecuteNonQuery();
        }
		connection.Close();
	}

    public DreamDTO GetDreamById(int dreamId)
    {
        DreamDTO dto = null;

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
                "LEFT JOIN tag ON rest.TagId = tag.TagId " +
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
}
