using System.ComponentModel.DataAnnotations;

namespace DataAccessDDO.ModelsDTO;

public class RestDTO
{
	public int RestId { get; set; }
	public int DreamId { get; set; }
	public int TagId { get; set; }

	[MaxLength(255)]
	public string RestName { get; set; }
}
