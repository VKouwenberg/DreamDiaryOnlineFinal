using System.ComponentModel.DataAnnotations;

namespace DataAccessDDO.ModelsDTO;

public class TagDTO
{
    [Key]
    public int TagId { get; set; } 

    [MaxLength(255)]
    public string TagName { get; set; }
}
