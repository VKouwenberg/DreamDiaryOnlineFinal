using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessDDO.ModelsDTO;

namespace DataAccessDDO.Repositories.DataAccessInterfaces;

public interface ITagRepository
{
	int CreateTag(TagDTO dto);
	void DeleteDreamTags(int dreamId);
	void DeleteTagsByDreamId(int dreamId);
}
