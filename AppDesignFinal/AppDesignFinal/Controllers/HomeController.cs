using AppDomain.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppDesignFinal.Controllers
{
	public class HomeController : Controller
	{
		private ApplicationDbContext db = new ApplicationDbContext();

		public ActionResult Index(string search)
		{
			var files = db.UploadFiles.AsQueryable();

			if (!string.IsNullOrEmpty(search))
			{
				files = files.Where(f =>
					f.FileName.Contains(search) ||
					f.FileType.Contains(search));
			}

			return View(files.ToList());
		}

	}
}