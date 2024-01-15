using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicDDO.Models;
using LogicDDO.Services.DataAccessRepositoriesInterfaces;

namespace LogicDDO.Services;

public class RestService : IRestRepository
{
    private readonly IRestRepository _restRepo;

    public RestService(IRestRepository restRepo)
    {
        _restRepo = restRepo;
    }

	public void CreateRest(int tagId, int dreamId)
	{
		_restRepo.CreateRest(tagId, dreamId);
	}

	public void DeleteRestByDreamId(int dreamId)
	{
		_restRepo.DeleteRestByDreamId(dreamId);
	}

	public void DeleteRestByTagIdAndDreamId(int tagId, int dreamId)
	{
		_restRepo.DeleteRestByTagIdAndDreamId(tagId, dreamId);
	}
}
