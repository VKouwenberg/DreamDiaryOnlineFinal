using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessDDO.ModelsDTO;
using DataAccessDDO.Repositories;

namespace DataAccessDDO.Repositories.DataAccessInterfaces;

public interface IDreamRepository
{
	List<DreamDTO> GetAllDreams();
	void CreateDream(DreamDTO dto);
	void UpdateDream(DreamDTO dto);
	void DeleteDream(int id);
	DreamDTO GetDreamById(int dreamId);
}
