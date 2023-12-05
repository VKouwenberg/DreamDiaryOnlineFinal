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

    public int DreamerId { get; set; } //fk
    public DreamerDTO DreamerDTO { get; set; } //nav
}