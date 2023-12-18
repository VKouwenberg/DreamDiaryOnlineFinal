using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessDDO.ModelsDTO
{
    public class TagDTO
    {
        [Key]
        public int TagId { get; set; } 

        [Required]
        [MaxLength(255)]
        public string TagName { get; set; }

        public int RestId { get; set; } 
    }
}
