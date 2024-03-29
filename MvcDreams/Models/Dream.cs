﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcDreams.Models
{
    public class Dream
    {
        public int Id { get; set; } //pk

        [StringLength(60, MinimumLength = 3)]
        public string? Name { get; set; } //Title

		[RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$")]
		[Required]
		[StringLength(30)]
		public string? ReadableBy { get; set; } //Genre //tag

		[RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$")]
		[Required]
		public string? DreamText { get; set; }
	}
}
