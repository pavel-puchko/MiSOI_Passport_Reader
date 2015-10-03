using Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MiSOI_Passport_Reader.Controllers
{
	public class HomeController : Controller
	{
		private readonly BoxFilter _boxFilter;
		private readonly MedianFilter _medianFilter;


		public HomeController()
		{
			_boxFilter = new BoxFilter();
			_medianFilter = new MedianFilter();
        }

		public ActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public ActionResult UploadImage()
		{
			for (int i = 0; i < Request.Files.Count; i++)
			{
				HttpPostedFileBase file = Request.Files[i]; //Uploaded file
															//Use the following properties to get file's name, size and MIMEType
				int fileSize = file.ContentLength;
				string fileName = file.FileName;
				string mimeType = file.ContentType;
				System.IO.Stream fileContent = file.InputStream;
				Image outputImageBox = _boxFilter.Transform(new Bitmap(Image.FromStream(file.InputStream)), 15); // Box Filter
				Image outputImageMedian = _medianFilter.UseFilter(new Bitmap(Image.FromStream(file.InputStream)), 15); // Median Filter


				outputImageBox.Save(Server.MapPath("~/img/BoxFilter_") + fileName); //File will be saved in application root
				outputImageMedian.Save(Server.MapPath("~/img/MedianFilter_") + fileName); //File will be saved in application root

			}
			return Json("Uploaded " + Request.Files.Count + " files");
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
	}
}