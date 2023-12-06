using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MvcDream.Models;

namespace MvcDream.Data
{
    public class MvcDreamContext : DbContext
    {
        public MvcDreamContext (DbContextOptions<MvcDreamContext> options)
            : base(options)
        {
        }

        public DbSet<MvcDream.Models.DreamViewModel> DreamViewModel { get; set; } = default!;
    }
}
