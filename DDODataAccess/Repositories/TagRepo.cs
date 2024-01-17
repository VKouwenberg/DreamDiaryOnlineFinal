using DataAccessDDO.DatabaseSettings;
using DataAccessDDO.DatabaseConfiguration;
using DataAccessDDO.ModelsDTO;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using LogicDDO.Services.DataAccessRepositoriesInterfaces;
using LogicDDO.Models;
using LogicDDO.ModelsDataAccessDTOs;
using DataAccessDDO.Repositories.DataAccessRepositoriesInterfaces;

namespace DataAccessDDO.Repositories;

public class TagRepo : ITagRepository, ITagRepo
{
    private readonly DatabaseSettings.DatabaseSettings _databaseSettings;
    private readonly IRestRepo _restRepo;

    public TagRepo(IOptions<DatabaseSettings.DatabaseSettings> databaseSettings, 
        IRestRepo restRepo)
    {
        _databaseSettings = databaseSettings.Value;
        _restRepo = restRepo;
    }

	public int CreateTag(TagDTOLogic tag)
	{
        TagDTO dto = MapTagDTOLogicToTagDTO(tag);

		MySqlConnection connection = new MySqlConnection(_databaseSettings.DefaultConnection);
		connection.Open();

		string query = "INSERT INTO Tag (TagName) VALUES (@TagName); SELECT LAST_INSERT_ID()";

		using MySqlCommand command = new MySqlCommand(query, connection);
		command.Parameters.AddWithValue("@TagName", dto.TagName);

		int newTagId = Convert.ToInt32(command.ExecuteScalar());

		connection.Close();

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

    //mapping
    public TagDTO MapTagDTOLogicToTagDTO(TagDTOLogic tag)
    {
        TagDTO tagDTO = new TagDTO
        {
            TagId = tag.TagId,
            TagName = tag.TagName
        };   
        return tagDTO;
    }

    public List<TagDTO> MapTagDTOLogicsToTagDTOs(List<TagDTOLogic> tags)
    {
        List<TagDTO> tagDTOs = new List<TagDTO>();
        foreach (TagDTOLogic tag in tags)
        {
            TagDTO dto = MapTagDTOLogicToTagDTO(tag);
            tagDTOs.Add(dto);
        }
        return tagDTOs;
    }

    public TagDTOLogic MapTagDTOToTagDTOLogic(TagDTO dto)
    {
        TagDTOLogic tag = new TagDTOLogic
        {
            TagId = dto.TagId,
            TagName = dto.TagName
        };
        return tag;
    }

    public List<TagDTOLogic> MapTagDTOsToTagDTOLogics(List<TagDTO> dTOs)
    {
        List<TagDTOLogic> tags = new List<TagDTOLogic>();
        foreach (TagDTO tagDTO in dTOs)
        {
            TagDTOLogic tag = MapTagDTOToTagDTOLogic(tagDTO);
            tags.Add(tag); 
        }
        return tags;
    }
}
