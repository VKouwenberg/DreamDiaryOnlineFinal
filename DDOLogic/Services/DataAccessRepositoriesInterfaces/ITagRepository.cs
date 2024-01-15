using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicDDO.Models;

namespace LogicDDO.Services.DataAccessRepositoriesInterfaces;

public interface ITagRepository
{
	int CreateTag(Tag tag);
	void DeleteDreamTags(int dreamId);
	void DeleteTagsByDreamId(int dreamId);
}
