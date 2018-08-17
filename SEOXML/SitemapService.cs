using SEOXML.Models;
using System;
using System.Collections.Generic;

namespace SEOXML
{
    public interface ISitemapService
    {
        List<SitemapItem> GenerateSitemap(string baseUrl);
    }

    public class SitemapService : ISitemapService
    {
        public SitemapService()
        {
            
        }

        public List<SitemapItem> GenerateSitemap(string baseUrl)
        {
            var sitemapItems = new List<SitemapItem>();

            for (int i = 0; i < 250; i++)
            {
                sitemapItems.Add(new SitemapItem(baseUrl + "/Page" + i.ToString(), changeFrequency: SitemapChangeFrequency.Always, priority: 1.0));
            }

            return sitemapItems;
        }
    }
}