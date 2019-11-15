using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SEOXML;
using SEOXML.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestSite.Controllers
{
    public class SitemapController : Controller
    {
        readonly ISitemapService sitemapService;
        

        public SitemapController(ISitemapService sitemapService)
        {
            this.sitemapService = sitemapService;
        }

        public IActionResult Index()
        {
            var sitemapData = new List<SitemapItem>();

            sitemapData.AddRange(GeneralPages("https://someurl.com"));

            var sitemap = sitemapService.GenerateSitemap(data =>
            {
                data.AddRange(sitemapData);
            });

            return Content(sitemap, "application/xml");
        }

        private List<SitemapItem> GeneralPages(string baseUrl)
        {
            var sitemapData = new List<SitemapItem>();

            sitemapData.Add(new SitemapItem(baseUrl));

            return sitemapData;
        }
    }
}
