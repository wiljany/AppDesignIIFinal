using AppDomain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDomain.Concrete
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext() : base("DefaultConnection")
		{
		}

		public DbSet<UploadFile> UploadFiles { get; set; }

	}
}
