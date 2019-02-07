using Microsoft.AspNetCore.Mvc;
using SEOXML.Models;
using System.Threading.Tasks;

namespace SEOXML.Controllers
{
    public class SitemapController : Controller
    {
        readonly ISitemapService sitemapService;

        public SitemapController(ISitemapService sitemapService)
        {
            this.sitemapService = sitemapService;
        }

        public async Task<IActionResult> Index()
        {
            string baseUrl = Request.Scheme + "://" + Request.Host.Value;
            return new SitemapResult(await sitemapService.GenerateSitemap(baseUrl));
        }
    }
}