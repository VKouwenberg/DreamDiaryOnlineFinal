using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessDDO.ModelsDTO
{
    public class RestDTO 
    {
        public int RestId { get; set; }
		public int DreamId { get; set; }
		public int TagId { get; set; } 

        [MaxLength(255)]
        public string RestName { get; set; }
    }
}
