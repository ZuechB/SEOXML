# SEOXML

This project was designed to make generating the sitemap XML easier for .net core. To get started:

Within your project update the startup.cs file to include:
<pre><code>

services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
services.AddScoped<ISitemapService, SitemapService>();

</code></pre>

Next create your own SitemapController and add the following:

<pre><code>

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
        return sitemapService.GenerateSitemap();
    }
}

</code></pre>

Now when you run your project and type in /sitemap all your controllers, actions, and routes will display with their defaults. 

If you would like to include your own dynamic sitemap data you can include it within your sitemap controller with the following:



<pre><code>

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

</code></pre>


The following attributes can be used on actions 
[HideSEO] - Doesn't include in the sitemap xml
[Route("/Changedtheurl")] - While it changes the route of your page it will also change the route of the xml file
[SEO(SitemapChangeFrequency.Daily, 1.0)] - Allows you to change the frequency and priority within the sitemap xml for each action



