using AppDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppDomain.Concrete;
//using AppDomain.Entities;
using System.Data.Entity;
using AppDesignFinal.Helpers;

namespace AppDesignFinal.Controllers
{
	[Authorize]
	public class AdminController : Controller
    {
		private ApplicationDbContext db = new ApplicationDbContext();


		public ActionResult Index()
        {
			if (Session["UserEmail"] == null)
				return RedirectToAction("Login", "Account");
			var files = db.UploadFiles.ToList();
			return View(files);

		}
		
		// GET: /File/Admin
		public ActionResult Admin()
		{
			if (Session["UserEmail"] == null)
				return RedirectToAction("Login", "Account");
			var files = db.UploadFiles.ToList();
			return View(files);
		}

		// GET: /File/Upload
		public ActionResult Upload() => View();

		// POST: /File/Upload
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Upload(UploadFile model, HttpPostedFileBase file)
		{
			if (Session["UserEmail"] == null)
				return RedirectToAction("Login", "Account");
			if (ModelState.IsValid && file != null)
			{
				using (var reader = new System.IO.BinaryReader(file.InputStream))
				{
					model.FileData = reader.ReadBytes(file.ContentLength);
				}
				model.FileName = file.FileName;
				model.MimeType = file.ContentType;
				model.FileType = GetFileTypeFromMime(file.ContentType);
				model.UploadDate = DateTime.Now;

				db.UploadFiles.Add(model);
				db.SaveChanges();
				return RedirectToAction("Admin");
			}
			return View(model);
		}

		// GET: /File/Edit/5
		public ActionResult Edit(int id)
		{
			if (Session["UserEmail"] == null)
				return RedirectToAction("Login", "Account");
			var file = db.UploadFiles.Find(id);
			if (file == null) return HttpNotFound();
			return View(file);
		}

		// POST: /File/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(UploadFile model)
		{
			if (Session["UserEmail"] == null)
				return RedirectToAction("Login", "Account");
			if (ModelState.IsValid)
			{
				db.Entry(model).State = EntityState.Modified;
				db.SaveChanges();
				return RedirectToAction("Admin");
			}
			return View(model);
		}

		// GET: /File/Delete/5
		public ActionResult Delete(int id)
		{
			if (Session["UserEmail"] == null)
				return RedirectToAction("Login", "Account");
			var file = db.UploadFiles.Find(id);
			if (file == null) return HttpNotFound();
			return View(file);
		}

		// POST: /File/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			if (Session["UserEmail"] == null)
				return RedirectToAction("Login", "Account");
			var file = db.UploadFiles.Find(id);
			db.UploadFiles.Remove(file);
			db.SaveChanges();
			return RedirectToAction("Admin");
		}

		// GET: /File/Details/5
		public ActionResult Details(int id)
		{
			if (Session["UserEmail"] == null)
				return RedirectToAction("Login", "Account");
			var file = db.UploadFiles.Find(id);
			if (file == null) return HttpNotFound();
			return View(file);
		}

		private string GetFileTypeFromMime(string mime)
		{
			if (mime.StartsWith("image/")) return "Image";
			if (mime == "application/pdf") return "PDF";
			if (mime.StartsWith("audio/")) return "Audio";
			if (mime.StartsWith("video/")) return "Video";
			return "Other";
		}
	}
}