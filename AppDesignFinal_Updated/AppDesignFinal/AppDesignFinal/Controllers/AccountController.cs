
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using AppDesignFinal.Models;
using AppDesignFinal.Helpers;
using AppDomain.Concrete;

namespace AppDesignFinal.Controllers
{
	public class AccountController : Controller
	{
		private ApplicationDbContext db = new ApplicationDbContext();

		//public ActionResult GenerateHash()
		//{
		//	string password = "admin2"; 
		//	string hash = PasswordHelper.HashPassword(password);
		//	return Content("Hashed password: " + hash);
		//}

		public ActionResult DebugLogin(string email, string password)
		{
			string hashed = PasswordHelper.HashPassword(password);
			var user = db.UploadFiles.FirstOrDefault(u => u.Email == email && u.Password == hashed);
			return Content(user != null ? "Login would succeed" : "No match found");
		}


		[HttpGet]
		public ActionResult Login()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Login(LoginViewModel model)
		{
			if (Session["UserEmail"] == null)
				return RedirectToAction("Login", "Account");
			{
				if (!ModelState.IsValid)
					return View(model);

				string hashedInput = PasswordHelper.HashPassword(model.Password);

				var user = db.UploadFiles.FirstOrDefault(u => u.Email == model.Email && u.Password == hashedInput);

				if (user != null)
				{
					Session["UserEmail"] = user.Email;
					return RedirectToAction("Index", "Home");
				}
				else
				{
					ModelState.AddModelError("", "Invalid login attempt.");
					return View(model);
				}
			}
		}
			public ActionResult Logout() 
			{
				if (Session["UserEmail"] == null)
					return RedirectToAction("Login", "Account");
				{
					Session.Clear();
					return RedirectToAction("Login");
				}
			}
		}
	}
