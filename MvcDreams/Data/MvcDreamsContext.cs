using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MvcDreams.Models;

namespace MvcDreams.Data
{
    public class MvcDreamsContext : DbContext
    {
        public MvcDreamsContext (DbContextOptions<MvcDreamsContext> options)
            : base(options)
        {


        }

        public DbSet<MvcDreams.Models.Dream> Dream { get; set; } = default!;
    }
}
