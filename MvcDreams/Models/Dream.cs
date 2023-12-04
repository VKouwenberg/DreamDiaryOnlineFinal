using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//test
namespace MvcDreams.Models
{
    public class Dream
    {
        public int Id { get; set; } //pk

        [StringLength(60, MinimumLength = 3)]
        public string? Name { get; set; } //Title

        [Display(Name = "Upload Date")]
        [DataType(DataType.Date)]
        public DateTime UploadDate { get; set; } //ReleaseDate

		[RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$")]
		[Required]
		[StringLength(30)]
		public string? ReadableBy { get; set; } //Genre

		[RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$")]
		[Required]
		[StringLength(30)]
		public string? Tag { get; set; }
		//public decimal Price { get; set; }

		[RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$")]
		[Required]
		public string? DreamText { get; set; }
	}
}
