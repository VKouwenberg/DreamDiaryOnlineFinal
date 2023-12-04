using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace MvcDreams.Models
{
    public class DreamTagViewModel
    {
        public List<Dream>? Dreams { get; set; }
        public SelectList? Tags { get; set; }
        public string? DreamTag { get; set; }
        public string? SearchString { get; set; }
    }
}
