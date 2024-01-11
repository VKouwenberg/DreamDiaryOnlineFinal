using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicDDO.Models;

namespace LogicDDO.Services.LogicInterfaces;
public interface IDreamService
{
	Dream GetDreamById(int id);
	List<Dream> GetDreams();
	void CreateDream(Dream dream);
	void DeleteDream(int id);
	void UpdateDream(Dream dream);
}
