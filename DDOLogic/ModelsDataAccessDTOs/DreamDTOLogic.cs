using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicDDO.ModelsDataAccessDTOs;
public class DreamDTOLogic
{
	public int DreamId { get; set; }
	public string DreamName { get; set; }
	public string DreamText { get; set; }
	public string ReadableBy { get; set; }
	public List<TagDTOLogic> Tags { get; set; }
}
