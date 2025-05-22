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
using System.IO;
using System.Net;

namespace AppDesignFinal.Controllers
{
	public class AdminController : Controller
    {
		private ApplicationDbContext db = new ApplicationDbContext();

		//[Authorize]

		public ActionResult Index()
        {
			var files = db.UploadFiles.ToList();
			return View(files);

		}
		
		// GET: /File/Admin
		public ActionResult Admin()
		{
			var files = db.UploadFiles.ToList();
			return View(files);
		}

		[HttpGet]
		public ActionResult Upload()
		{
			return View();
		}
		// POST: /File/Upload
		[HttpPost]
		public ActionResult Upload(HttpPostedFileBase file, string fileType)
		{
			if (file != null && file.ContentLength > 0)
			{
				var upload = new UploadFile
				{
					FileName = Path.GetFileName(file.FileName),
					FileType = fileType,
					MimeType = file.ContentType,
					FileData = new byte[file.ContentLength],
					ImageMimeType = file.ContentType.StartsWith("image/") ? file.ContentType : null
				};

				file.InputStream.Read(upload.FileData, 0, file.ContentLength);

				db.UploadFiles.Add(upload);
				db.SaveChanges();

				return RedirectToAction("Index");
			}

			ViewBag.Message = "No file selected";
			return View();
		}

		[HttpGet]
		public ActionResult Edit(int id)
		{
			var file = db.UploadFiles.Find(id);
			if (file == null) return HttpNotFound();
			return View(file);
		}

		// POST: /File/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(UploadFile model)
		{
			if (ModelState.IsValid)
			{
				db.Entry(model).State = EntityState.Modified;
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			return View(model);
		}

		[HttpGet]
		public ActionResult Delete(int? id)
		{
			if (id == null)
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			var file = db.UploadFiles.Find(id);
			if (file == null)
				return HttpNotFound();
			return View(file);
		}

		// POST: /File/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			var file = db.UploadFiles.Find(id);
			db.UploadFiles.Remove(file);
			db.SaveChanges();
			return RedirectToAction("Index");
		}

		// GET: /File/Details/5
		public ActionResult Details(int id)
		{
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