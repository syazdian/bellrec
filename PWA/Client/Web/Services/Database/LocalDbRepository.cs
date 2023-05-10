namespace Bell.Reconciliation.Frontend.Web.Services.Database;

public class LocalDbRepository : ILocalDbRepository
{
    private readonly ISqliteWasmDbContextFactory<StapleSourceContext> _dbContextFactory;

    public LocalDbRepository(ISqliteWasmDbContextFactory<StapleSourceContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public async Task<int> InsertDataToLocalDbAsync(BellStaplesSourceDto bellStaplesSources)
    {
        try
        {
            using var ctx = await _dbContextFactory.CreateDbContextAsync();

            await ctx.BellSources.AddRangeAsync(bellStaplesSources.BellSources.ToList());
            await ctx.StaplesSources.AddRangeAsync(bellStaplesSources.StaplesSources.ToList());
            await ctx.SaveChangesAsync();
            return bellStaplesSources.BellSources.Max(x => x.Id);
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task<List<BellSourceDto>> GetBellSourceFromLocalDb()
    {
        using var ctx = await _dbContextFactory.CreateDbContextAsync();
        List<BellSourceDto> bellSources = await ctx.BellSources.ToListAsync();
        return bellSources;
    }

    public async Task<List<StaplesSourceDto>> GetStapleSourceFromLocalDb()
    {
        using var ctx = await _dbContextFactory.CreateDbContextAsync();
        List<StaplesSourceDto> staplesSources = await ctx.StaplesSources.ToListAsync();
        return staplesSources;
    }
}