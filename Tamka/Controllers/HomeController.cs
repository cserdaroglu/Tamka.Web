using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Mvc;
using Tamka.Models;

namespace Tamka.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}

		public ActionResult About()
		{
			return View();
		}

		public ActionResult Transform()
		{
			return View();
		}

		public ActionResult RegionIst()
		{
			return View();
		}

		public ActionResult RegionAnk()
		{
			return View();
		}

		public ActionResult Contact()
		{
			return View();
		}

		public ActionResult Ilan()
		{
			return View();
		}

		[HttpPost]
		public ActionResult SendMail(Mail eposta)
		{
			try
			{
				MailMessage mail = new MailMessage();
				SmtpClient smtp = new SmtpClient();
				smtp.Credentials = new NetworkCredential("info@tamkayapi.com", "R_1si@1l@-6D2QdC");
				smtp.Port = 587;
				smtp.Host = "mail.tamkayapi.com";
				smtp.EnableSsl = false;
				mail.IsBodyHtml = true;
				mail.From = new MailAddress("info@tamkayapi.com");
				mail.To.Add("info@tamkayapi.com");
				mail.Body = "Ad-Soyad : " + eposta.name + " \n Mesaj : " + eposta.body + " \n E-Posta : " + eposta.email + " ";
				smtp.Send(mail);
				HttpContext.Session.SetString("mail", "1");
				return RedirectToAction("Contact");
			}
			catch (Exception)
			{
				HttpContext.Session.SetString("mail", "2");
				return RedirectToAction("Contact");
			}
		}
	}
}
