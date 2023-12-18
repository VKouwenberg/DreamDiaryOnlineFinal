using System.ComponentModel.DataAnnotations;
using System.Security.Policy;

namespace DataAccessDDO.ModelsDTO;

public class DreamDTO
{
	[Key]
	public int DreamId { get; set; } //pk

	[Required]
	[MaxLength(255)]
	public string DreamName { get; set; }

	[Required]
	public string DreamText { get; set; }

	[Required]
	public string ReadableBy { get; set; }
	public List<TagDTO> Tags { get; set; }
}