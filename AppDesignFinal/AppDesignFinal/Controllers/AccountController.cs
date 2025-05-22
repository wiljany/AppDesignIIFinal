using System.Linq;
using System.Web.Mvc;
using AppDesignFinal.Models;
using AppDomain.Concrete;
using AppDomain.Entities;
using AppDesignFinal.Helpers;

namespace WebCapstoneFinal.Controllers
{
	public class AccountController : Controller
	{
		private ApplicationDbContext db = new ApplicationDbContext();

		[HttpGet]
		public ActionResult Login()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Login(LoginViewModel model)
		{
			if (!ModelState.IsValid)
				return View(model);

			string hashed = PasswordHelper.HashPassword(model.Password);
			var user = db.Users.FirstOrDefault(u => u.Email == model.Email && u.Password == hashed);

			if (user != null)
			{
				Session["UserEmail"] = user.Email;
				return RedirectToAction("Index", "Admin");
			}

			ModelState.AddModelError("", "Invalid login attempt.");
			return View(model);
		}

		public ActionResult Logout()
		{
			Session.Clear();
			return RedirectToAction("Login");
		}
	}
}
