using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SampleSEOSite.Models;
using SEOXML.Models;

namespace SampleSEOSite.Controllers
{
    public class HomeController : Controller
    {
        [SEO(SitemapChangeFrequency.Daily, 1.0)]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public object Apples()
        {
            return Content("");
        }

        [Route("/Changedtheurl")]
        public IActionResult Privacy()
        {
            return View();
        }

        [HttpDelete]
        public IActionResult LookAtMe()
        {
            return View();
        }

        [HttpPut]
        public IActionResult ThisIsPutOffIsh()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Test()
        {
            await Task.Delay(1000);
            return Json(true);
        }

        public async Task<IActionResult> AnotherTest() // this one should be showing but isn't because of Task
        {
            await Task.Delay(1000);
            return View();
        }

        [HideSEO]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
