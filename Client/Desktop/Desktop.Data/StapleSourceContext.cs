namespace Bell.Reconciliation.Frontend.Desktop.Data;

public class StapleSourceContext : DbContext
{
    public StapleSourceContext(DbContextOptions<StapleSourceContext> options) : base(options)
    {
        Database.Migrate();
    }

    // It is required to override this method when adding/removing migrations from class library
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite();

    public DbSet<StaplesSourceDto> StaplesSources { get; set; }
    public DbSet<BellSourceDto> BellSources { get; set; }
}