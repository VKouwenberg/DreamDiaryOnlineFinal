using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessDDO.ModelsDTO
{
    public class RestDTO //koppeltabel
    {
        public int RestId { get; set; }
		public int DreamId { get; set; } //fk
		public int TagId { get; set; } //fk

        [MaxLength(255)]
        public string RestName { get; set; }
    }
}
