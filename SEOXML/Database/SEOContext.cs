using Microsoft.EntityFrameworkCore;

namespace SEOXML.Database
{
    public class SEOContext : DbContext
    {
        public SEOContext(DbContextOptions<SEOContext> options) : base(options) { }

        public DbSet<SEO> SEOViews { get; set; }
    }
}
