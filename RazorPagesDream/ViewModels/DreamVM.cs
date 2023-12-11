using System.ComponentModel.DataAnnotations;

namespace RazorPagesDream.ViewModels
{
    public class DreamVM
    {
        public int Id { get; set; }
        public string DreamName { get; set; }
        public string DreamText { get; set; }
        public string ReadableBy { get; set; }
        public int DreamerId { get; set; }
    }
}
