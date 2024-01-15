using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicDDO.Models;

namespace LogicDDO.Services.DataAccessRepositoriesInterfaces;

public interface IDreamRepository
{
	List<Dream> GetAllDreams();
	void CreateDream(Dream dto);
	void UpdateDream(Dream dto);
	void DeleteDream(int id);
	Dream GetDreamById(int id);
}
