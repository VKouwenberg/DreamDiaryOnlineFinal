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
		public int DreamId { get; set; } //fk
		public int TagId { get; set; } //fk

        [MaxLength(255)]
        public string RestName { get; set; }

        public DreamDTO DreamDTO { get; set; } //nav
        public TagDTO TagDTO { get; set; } //nav
    }
}
