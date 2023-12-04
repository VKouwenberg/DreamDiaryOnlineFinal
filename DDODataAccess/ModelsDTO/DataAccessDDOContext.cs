using Microsoft.EntityFrameworkCore;
using MySql.EntityFrameworkCore.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Test

namespace DataAccessDDO.ModelsDTO
{
	public class DataAccessDDOContext : DbContext
	{
		public DataAccessDDOContext(DbContextOptions<DataAccessDDOContext> options) 
			: base(options) 
		{

		}

		public DbSet<DreamDTO> Dreams { get; set; }
		public DbSet<RestDTO> Rests { get; set; }
		public DbSet<DreamerDTO> Dreamers { get; set; }
		public DbSet<TagDTO> Tags { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			// Specify the MySQL connection string
			optionsBuilder.UseMySQL("Server=localhost;Database=ddodb;User Id=root;");
		}
	}
}
