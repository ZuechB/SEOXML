using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SEOXML;
using SEOXML.Models;

namespace SampleSEOSite.Controllers
{
    public class SitemapController : Controller
    {
        readonly ISitemapService sitemapService;

        public SitemapController(ISitemapService sitemapService)
        {
            this.sitemapService = sitemapService;
        }

        [HideSEO]
        public IActionResult Index()
        {
            var sitemapData = new List<SitemapItem>();

            sitemapData.Add(new SitemapItem("/specialpage"));


            return sitemapService.GenerateSitemap(data =>
            {
                data.AddRange(sitemapData);
            });
        }
    }
}