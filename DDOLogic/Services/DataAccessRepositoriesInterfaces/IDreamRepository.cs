using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicDDO.Models.DataAccessModelInterfaces;

namespace LogicDDO.Services.DataAccessRepositoriesInterfaces;

public interface IDreamRepository
{
	List<IDreamDTO> GetAllDreams();
	void CreateDream(IDreamDTO dto);
	void UpdateDream(IDreamDTO dto);
	void DeleteDream(int id);
	IDreamDTO GetDreamById(int id);
}
