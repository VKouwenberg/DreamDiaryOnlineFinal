using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessDDO.ModelsDTO;

namespace DataAccessDDO.Repositories.DataAccessInterfaces;

public interface IRestRepository
{
	void CreateRest(int tagId, int dreamId);
	void DeleteRestByDreamId(int dreamId);
	void DeleteRestByTagIdAndDreamId(int tagId, int dreamId);
}
