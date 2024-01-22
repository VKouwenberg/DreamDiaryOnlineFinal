using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicDDO.Models;
using LogicDDO.Services.DataAccessRepositoriesInterfaces;
using LogicDDO.Services.LogicServicesInterfaces;
using LogicDDO.ModelsDataAccessDTOs;

namespace LogicDDO.Services;

public class RestService : IRestService
{
    private readonly IRestRepository _restRepository;

    public RestService(IRestRepository restRepo)
    {
        _restRepository = restRepo;
    }

	public void CreateRest(int tagId, int dreamId)
	{
		_restRepository.CreateRest(tagId, dreamId);
	}

	public void DeleteRestByDreamId(int dreamId)
	{
		_restRepository.DeleteRestByDreamId(dreamId);
	}

	public void DeleteRestByTagIdAndDreamId(int tagId, int dreamId)
	{
		_restRepository.DeleteRestByTagIdAndDreamId(tagId, dreamId);
	}

	/*public Rest MapDTOLogicTagToTag(RestDTOLogic tagDTO)
	{
		Rest tag = new Rest
		{
			TagId = tagDTO.TagId,
			TagName = tagDTO.TagName
		};
		return tag;
	}

	public List<Rest> MapDTOLogicTagsToTags(List<RestDTOLogic> tagDTOs)
	{
		List<Rest> tags = new List<Rest>();
		foreach (RestDTOLogic tagDTO in tagDTOs)
		{
			Rest tag = MapDTOLogicTagToTag(tagDTO);
			tags.Add(tag);
		}
		return tags;
	}*/
}
