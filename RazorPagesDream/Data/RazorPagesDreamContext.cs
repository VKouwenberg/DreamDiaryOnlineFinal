using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RazorPagesDream.ViewModels;

namespace RazorPagesDream.Data
{
    public class RazorPagesDreamContext : DbContext
    {
        public RazorPagesDreamContext (DbContextOptions<RazorPagesDreamContext> options)
            : base(options)
        {
        }

        public DbSet<RazorPagesDream.ViewModels.DreamVM> DreamVM { get; set; } = default!;
    }
}
