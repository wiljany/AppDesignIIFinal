using AppDomain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppDesignFinal.Models;

namespace AppDomain.Concrete
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext() : base("AppFinal")
		{
		}

		public DbSet<UploadFile> UploadFiles { get; set; }
		public DbSet<Users> Users { get; set; }
	}
}
