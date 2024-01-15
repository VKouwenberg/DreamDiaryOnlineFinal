using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicDDO.Models;

namespace LogicDDO.Services.DataAccessRepositoriesInterfaces;

public interface IRestRepository
{
	void CreateRest(int tagId, int dreamId);
	void DeleteRestByDreamId(int dreamId);
	void DeleteRestByTagIdAndDreamId(int tagId, int dreamId);
}
