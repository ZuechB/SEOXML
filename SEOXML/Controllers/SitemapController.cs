using Microsoft.AspNetCore.Mvc;
using SEOXML.Models;

namespace SEOXML.Controllers
{
    public class SitemapController : Controller
    {
        readonly ISitemapService sitemapService;
        private const string baseUrl = "www.mywebsite.com";

        public SitemapController(ISitemapService sitemapService)
        {
            this.sitemapService = sitemapService;
        }

        public IActionResult Index()
        {
            return new SitemapResult(sitemapService.GenerateSitemap(baseUrl));
        }
    }
}
