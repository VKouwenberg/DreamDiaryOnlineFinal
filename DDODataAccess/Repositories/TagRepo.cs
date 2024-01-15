
using DataAccessDDO.DatabaseSettings;
using DataAccessDDO.ModelsDTO;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using LogicDDO.Services.DataAccessRepositoriesInterfaces;
using LogicDDO.Models;

namespace DataAccessDDO.Repositories;

public class TagRepo : ITagRepository
{
    private readonly DatabaseSettings.DatabaseSettings _databaseSettings;
    private readonly RestRepo _restRepo;

    public TagRepo(IOptions<DatabaseSettings.DatabaseSettings> databaseSettings, 
        RestRepo restRepo)
    {
        _databaseSettings = databaseSettings.Value;
        _restRepo = restRepo;
    }

	public int CreateTag(Tag tag)
	{
        TagDTO dto = MapTagToTagDTO(tag);

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

    public TagDTO MapTagToTagDTO(Tag tag)
    {
        TagDTO tagDTO = new TagDTO
        {
            TagId = tag.TagId,
            TagName = tag.TagName
        };   
        return tagDTO;
    }

    public List<TagDTO> MapTagsToTagDTOs(List<Tag> tags)
    {
        List<TagDTO> tagDTOs = new List<TagDTO>();
        foreach (Tag tag in tags)
        {
            TagDTO dto = MapTagToTagDTO(tag);
            tagDTOs.Add(dto);
        }
        return tagDTOs;
    }

    public Tag MapTagDTOToTag(TagDTO dto)
    {
        Tag tag = new Tag
        {
            TagId = dto.TagId,
            TagName = dto.TagName
        };
        return tag;
    }

    public List<Tag> MapTagDTOsToTags(List<TagDTO> dTOs)
    {
        List<Tag> tags = new List<Tag>();
        foreach (TagDTO tagDTO in dTOs)
        {
            Tag tag = MapTagDTOToTag(tagDTO);
            tags.Add(tag); 
        }
        return tags;
    }
}
