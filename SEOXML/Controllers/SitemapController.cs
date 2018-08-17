using Microsoft.AspNetCore.Mvc;
using SEOXML.Models;

namespace SEOXML.Controllers
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
            var baseUrl = HttpContext.Request.Host.Value;
            return new SitemapResult(sitemapService.GenerateSitemap(baseUrl));
        }
    }
}
