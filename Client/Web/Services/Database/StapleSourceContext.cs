using Microsoft.EntityFrameworkCore;
using Bell.Reconciliation.Common.Models;

namespace Bell.Reconciliation.Frontend.Web.Database
{
    public class StapleSourceContext : DbContext
    {
        public StapleSourceContext(DbContextOptions<StapleSourceContext> opts) : base(opts)
        {
        }

        public DbSet<BellSourceDto> BellSources { get; set; } = null!;
        public DbSet<StaplesSourceDto> StaplesSources { get; set; } = null!;
        public DbSet<SyncLogsDto> SyncLogs { get; set; } = null!;
    }
}