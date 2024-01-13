using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TestBootstrapRazorPages.ViewModels;

public class DreamVM
{
    public int Id { get; set; }

    [DisplayName("Dream Name")]
	[Required]
	public string DreamName { get; set; }

    [DisplayName("Dream")]
    [Required]
    public string DreamText { get; set; }

    [DisplayName("Readable By")]
	[Required]
	public string ReadableBy { get; set; }

    public List<TagVM> Tags { get; set; }
}

