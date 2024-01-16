using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicDDO.ModelsDataAccessDTOs;

namespace LogicDDO.Services.LogicServicesInterfaces;

public interface IRestService
{
	void CreateRest(int tagId, int dreamId);
	void DeleteRestByDreamId(int dreamId);
	void DeleteRestByTagIdAndDreamId(int tagId, int dreamId);
}
