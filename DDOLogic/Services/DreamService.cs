using LogicDDO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessDDO.ModelsDTO;
using DataAccessDDO.Repositories;
using Org.BouncyCastle.Bcpg; //what even is this

namespace LogicDDO.Services;

public class DreamService
{
	//private readonly List<Dream> _dreamList;
	private readonly DreamRepo _dreamRepo;
	private readonly TagService _tagService;

	public DreamService(DreamRepo dreamRepo, TagService tagService)
	{
		_dreamRepo = dreamRepo;
		_tagService = tagService;
	}

	//Calls private mapping method that converts DTOs to logic models
	//The mapping method is private because otherwise I'd have to link the DAL to the view
	//Now you can Get Dreams without having to pass on a list
	public List<Dream> GetDreams()
	{
		List<Dream> dreams = MapDreamDTOsToDreams(_dreamRepo.GetAllDreams());

        /*Console.WriteLine("GetDreams Logic tag names");
        foreach (Dream dream in dreams)
        {
			if (dream.Tags != null)
			{
                foreach (Tag tag in dream.Tags)
                {
                    Console.WriteLine(tag.TagName.ToString());
                }
            }
			
        }*/
        return dreams;
    }

	private Dream MapDreamDTOToDream(DreamDTO dto)
	{
		//Dreamer dreamer = new Dreamer(dto.DreamerId, dto.DreamerName);
		Dream dream = new Dream
		{
			DreamName = dto.DreamName,
			DreamText = dto.DreamText,
			ReadableBy = dto.ReadableBy,
			DreamId = dto.DreamId
		};
		if (dto.Tags != null)
		{
			List<Tag> tags = _tagService.MapTagDTOsToTags(dto.Tags);

			/*Console.WriteLine("Individual dto to dream map MapDreamDTOToDream");
			foreach (Tag tag in tags)
			{
				Console.WriteLine(tag.TagName.ToString());
			}*/

        }


		return dream;
	}


	//Maps DTOs to logic models
	private List<Dream> MapDreamDTOsToDreams(List<DreamDTO> dTOs)
	{/*
		List<DreamDTO> dreamDTOs = _dreamRepo.GetAllDreams();
*/
		List<Dream> dreams = new List<Dream>();

		foreach (DreamDTO dto in dTOs)
		{
			Dream dream = MapDreamDTOToDream(dto);
			dreams.Add(dream);
		}

        /*Console.WriteLine("Maps dreamDTOs to dreams MapDreamDTOsToDreams");
        foreach (Dream dream in dreams)
        {
			if (dream.Tags != null)
			{
                foreach (Tag tag in dream.Tags)
                {
                    Console.WriteLine(tag.TagName.ToString());
                }
            }
        }*/

        return dreams;
	}

	//maps logic model to DTO
	private DreamDTO MapToDTO(Dream dream)
	{
		return new DataAccessDDO.ModelsDTO.DreamDTO
		{
			DreamId = dream.DreamId,
			DreamName = dream.DreamName,
			DreamText = dream.DreamText,
			ReadableBy = dream.ReadableBy
		};
	}

	//CRUD
	public void CreateDream(Dream dream)
	{
		var dreamDTO = MapToDTO(dream);
		_dreamRepo.CreateDream(dreamDTO);
	}

	public void UpdateDream(Dream dream)
	{
		var dreamDTO = MapToDTO(dream);
		_dreamRepo.UpdateDream(dreamDTO);
	}

	public void DeleteDream(int dreamId)
	{
		_dreamRepo.DeleteDream(dreamId);
	}
    //This creates logicmodels that are intended to be sent to the view as viewmodels
    //These only contain the data the view needs.
    // /It happens to be the same, but that is not always the case.
    /*private List<Dream> MapToViewModels(List<Dream> logicDreams)
	{
		List<Dream> dreamViewModels = new List<Dream>();

		foreach (var logicDream in logicDreams)
		{
			Dream dreamViewModel = new Dream
			{
				DreamId = logicDream.DreamId,
				DreamName = logicDream.DreamName,
				DreamText = logicDream.DreamText,
				ReadableBy = logicDream.ReadableBy
			};

			dreamViewModels.Add(dreamViewModel);
		}

		return dreamViewModels;
	}*/

    /*public List<Dream> GetDreamViewModels()
	{
		return MapToViewModels(_dreamList);
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
