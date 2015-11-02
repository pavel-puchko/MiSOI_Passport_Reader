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
		private readonly Binarization _binarizationFilter;



		public HomeController()
		{
			_boxFilter = new BoxFilter();
			_medianFilter = new MedianFilter();
			_binarizationFilter = new Binarization();
        }

		public ActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public ActionResult UploadImage()
		{
				HttpPostedFileBase file = Request.Files[0]; //Uploaded file
															//Use the following properties to get file's name, size and MIMEType
				int fileSize = file.ContentLength;
				string fileName = file.FileName;
				string mimeType = file.ContentType;
				System.IO.Stream fileContent = file.InputStream;
				Image outputImageBox = _boxFilter.Transform(new Bitmap(Image.FromStream(file.InputStream)), 9); // Box Filter
				Image outputImageMedian = _medianFilter.UseFilter(new Bitmap(Image.FromStream(file.InputStream)), 10); // Median Filter
				Image outputImageBinarization = _binarizationFilter.process(new Bitmap(Image.FromStream(file.InputStream)));

				outputImageBinarization.Save(Server.MapPath("~/img/BinarizationFilter_") + fileName); //File will be saved in application root
				outputImageBox.Save(Server.MapPath("~/img/BoxFilter_") + fileName); //File will be saved in application root
				outputImageMedian.Save(Server.MapPath("~/img/MedianFilter_") + fileName); //File will be saved in application root

			return Json(fileName);
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