using System.ComponentModel.DataAnnotations;
using System.Security.Policy;

namespace DataAccessDDO.ModelsDTO;

public class DreamerDTO
{
    [Key]
    public int DreamerId { get; set; } //pk

    [Required]
    public string DreamerName { get; set; }

    public List<DreamDTO> DreamDTOs { get; set; } //nav
}