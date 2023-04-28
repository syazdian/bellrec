using Bell.Reconciliation.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace Bell.Reconciliation.Frontend.Web.Database
{
    public class StapleSourceContext : DbContext
    {
        public StapleSourceContext(DbContextOptions<StapleSourceContext> opts) : base(opts)
        {
        }

        public DbSet<BellSource> BellSources { get; set; } = null!;
        public DbSet<StaplesSource> StaplesSources { get; set; } = null!;
    }
}