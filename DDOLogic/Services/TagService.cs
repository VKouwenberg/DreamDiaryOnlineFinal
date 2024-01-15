using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicDDO.Models;
using LogicDDO.Services.DataAccessRepositoriesInterfaces;

namespace LogicDDO.Services;

public class TagService : ITagRepository
{
    private readonly ITagRepository _tagRepo;

    public TagService(ITagRepository tagRepo)
    {
        _tagRepo = tagRepo;
    }

	public int CreateTag(Tag tag)
	{
		return _tagRepo.CreateTag(tag);
	}

	public void DeleteDreamTags(int dreamId)
	{
		_tagRepo.DeleteDreamTags(dreamId);
	}

	public void DeleteTagsByDreamId(int dreamId)
	{
		_tagRepo.DeleteTagsByDreamId(dreamId);
	}
}
