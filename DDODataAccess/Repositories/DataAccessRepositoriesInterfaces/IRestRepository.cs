using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessDDO.Repositories;
using DataAccessDDO.ModelsDTO;
using LogicDDO.ModelsDataAccessDTOs;

namespace DataAccessDDO.Repositories.DataAccessRepositoriesInterfaces;

public interface IRestRepository
{
	void CreateRest(int tagId, int dreamId);
	void DeleteRestByDreamId(int dreamId);
	void DeleteRestByTagIdAndDreamId(int tagId, int dreamId);
}
