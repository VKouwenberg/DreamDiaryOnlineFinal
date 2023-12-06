using System.ComponentModel.DataAnnotations;

namespace MvcDream.Models;

public class DreamViewModel
{
    public int Id { get; set; } //DreamId
    public string? DreamName { get; set; }
    public string? DreamText { get; set; }
    public string ReadableBy { get; set; }
    public int DreamerId { get; set; } //fk
    //public DreamerDTO Dreamer { get; set; }

    //Id int
    //Title string
    //ReleaseDate DateTime
    //Genre string
    //Price decimal
}

