using SEOXML.Models;
using System;

namespace SEOXML.Database
{
    public class SEO
    {
        public long Id { get; set; }
        public string Url { get; set; }
        public DateTime? LastModified { get; set; }
        public SitemapChangeFrequency? ChangeFrequency { get; set; }
        public double? Priority { get; set; }
        public bool IsDeactivated { get; set; }
    }
}
