using DataAccessDDO.ModelsDTO;
using DataAccessDDO.DatabaseConfiguration;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using LogicDDO.Models;
using LogicDDO.ModelsDataAccessDTOs;
using LogicDDO.Services;
using LogicDDO.Services.DataAccessRepositoriesInterfaces;
using DataAccessDDO.Repositories.DataAccessRepositoriesInterfaces;


namespace DataAccessDDO.Repositories;

public class DreamRepo : IDreamRepository, IDreamRepo
{
    private readonly DatabaseContext _databaseContext;

    private readonly DatabaseSettings.DatabaseSettings _databaseSettings;
    private readonly ITagRepo _tagRepo;
    private readonly IRestRepo _restRepo;

    public DreamRepo(IOptions<DatabaseSettings.DatabaseSettings> databaseSettings,
        ITagRepo tagRepo,
        IRestRepo restRepo)
    {
        _databaseContext = new DatabaseContext();

		_databaseSettings = databaseSettings.Value;
        _tagRepo = tagRepo;
        _restRepo = restRepo;
    }

    public List<DreamDTOLogic> GetAllDreams()
    {
        List<DreamDTO> dreams = new List<DreamDTO>();

        MySqlConnection connection = _databaseContext.GetConnection();

        /*MySqlConnection connection = new MySqlConnection(_databaseSettings.DefaultConnection);
        connection.Open();*/

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

		List<DreamDTOLogic> convertedDreams = new List<DreamDTOLogic>();

        convertedDreams = MapDreamDTOsToDreamDTOLogics(dreams);

		return convertedDreams;
    }

    public void CreateDream(DreamDTOLogic dream)
    {
		DreamDTO dto = MapDreamDTOLogicToDreamDTO(dream);

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

    public void UpdateDream(DreamDTOLogic dream)
    {
        DreamDTO dto = MapDreamDTOLogicToDreamDTO(dream);

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
                if (!string.IsNullOrWhiteSpace(tag.TagName))
                {
					//create the tag itself
                    TagDTOLogic tagDTOLogic = new TagDTOLogic();
                    tagDTOLogic = _tagRepo.MapTagDTOToTagDTOLogic(tag);

					int newTagId = _tagRepo.CreateTag(tagDTOLogic);

					//link the tag and the dreamId in a new rest
					_restRepo.CreateRest(newTagId, dto.DreamId);
				}
                else
                {
                    _restRepo.DeleteRestByTagIdAndDreamId(tag.TagId, dto.DreamId);
                }
            }
        }

        connection.Close();
    }

    public void DeleteDream(int dreamId)
    {
        using MySqlConnection connection = new MySqlConnection(_databaseSettings.DefaultConnection);

        connection.Open();

        _restRepo.DeleteRestByDreamId(dreamId);

        _tagRepo.DeleteTagsByDreamId(dreamId);

        string deleteDreamQuery = "DELETE FROM Dream WHERE DreamId = @DreamId";
        using MySqlCommand deleteDreamCommand = new MySqlCommand(deleteDreamQuery, connection);
        deleteDreamCommand.Parameters.AddWithValue("@DreamId", dreamId);

        deleteDreamCommand.ExecuteNonQuery();

        connection.Close();
	}

    public DreamDTOLogic GetDreamById(int dreamId)
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

        DreamDTOLogic convertedDTO = MapDreamDTOToDreamDTOLogic(dto);

        return convertedDTO;
    }

    //mapping
    public DreamDTO MapDreamDTOLogicToDreamDTO(DreamDTOLogic dream)
    {
        DreamDTO dto = new DreamDTO
        {
            DreamId = dream.DreamId,
            DreamName = dream.DreamName,
            DreamText = dream.DreamText
        };
        return dto;
    }

    public List<DreamDTO> MapDreamDTOLogicsToDreamDTOs(List<DreamDTOLogic> dreams)
    {
        List<DreamDTO> dTOs = new List<DreamDTO>();
        foreach (DreamDTOLogic dream in dreams)
        {
            DreamDTO dto = MapDreamDTOLogicToDreamDTO(dream);
            dTOs.Add(dto);
        }
        return dTOs;
    }

    public DreamDTOLogic MapDreamDTOToDreamDTOLogic(DreamDTO dto)
    {
        DreamDTOLogic dream = new DreamDTOLogic
        {
            DreamId= dto.DreamId,
            DreamName = dto.DreamName,
            DreamText = dto.DreamText,
            ReadableBy = dto.ReadableBy,
            Tags = _tagRepo.MapTagDTOsToTagDTOLogics(dto.Tags)
        };
        return dream;
    }

    public List<DreamDTOLogic> MapDreamDTOsToDreamDTOLogics(List<DreamDTO> dreamDTOs)
    {
        List<DreamDTOLogic> dreams = new List<DreamDTOLogic>();
        foreach (DreamDTO dto in dreamDTOs)
        {
            DreamDTOLogic dream = MapDreamDTOToDreamDTOLogic(dto);
            dreams.Add(dream);
        }
        return dreams;
    }
}
