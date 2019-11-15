using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace SEOXML.Models
{
    public interface ISitemapItem
    {
        /// <summary>
        /// URL of the page.
        /// </summary>
        string Url { get; }
        /// <summary>
        /// The date of last modification of the file.
        /// </summary>
        DateTime? LastModified { get; }
        /// <summary>
        /// How frequently the page is likely to change.
        /// </summary>
        SitemapChangeFrequency? ChangeFrequency { get; }
        /// <summary>
        /// The priority of this URL relative to other URLs on your site. Valid values range from 0.0 to 1.0.
        /// </summary>
        double? Priority { get; }
    }

    public enum SitemapChangeFrequency
    {
        Always,
        Hourly,
        Daily,
        Weekly,
        Monthly,
        Yearly,
        Never
    }

    //public class SitemapResult : ActionResult
    //{
    //    private readonly IEnumerable<ISitemapItem> items;
    //    private readonly ISitemapGenerator generator;

    //    public SitemapResult(IEnumerable<ISitemapItem> items) : this(items, new SitemapGenerator())
    //    {
    //    }

    //    public SitemapResult(IEnumerable<ISitemapItem> items, ISitemapGenerator generator)
    //    {
    //        //Ensure.Argument.NotNull(items, "items");
    //        //Ensure.Argument.NotNull(generator, "generator");
    //        this.items = items;
    //        this.generator = generator;
    //    }

    //    public override async Task ExecuteResultAsync(ActionContext context)
    //    {
    //        var sitemap = generator.GenerateSiteMap(items);
    //        sitemap.ToString();
    //        sitemap.WriteTo(writer);


    //        //var response = context.HttpContext.Response;
    //        //response.ContentType = "text/xml";

    //        //var anotherStream = new MemoryStream();
    //        //await response.Body.CopyToAsync(anotherStream);
    //        //anotherStream.Position = 0;

    //        //using (var writer = new XmlTextWriter(anotherStream, Encoding.UTF8))
    //        //{
    //        //    writer.Formatting = Formatting.Indented;
    //        //    var sitemap = generator.GenerateSiteMap(items);
    //        //    sitemap.WriteTo(writer);
    //        //}
    //    }
    //}
}