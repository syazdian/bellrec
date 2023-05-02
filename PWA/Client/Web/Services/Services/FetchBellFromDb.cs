using Bell.Reconciliation.Common.Models;
using Bell.Reconciliation.Common.Models.Domain;
using Bell.Reconciliation.Common.Utilities;
using Bell.Reconciliation.Frontend.Shared.ServiceInterfaces;
using Bell.Reconciliation.Frontend.Web.Database;
using SqliteWasmHelper;

namespace Bell.Reconciliation.Frontend.Web.Services;

public class FetchBellFromDb : IInjectBellSource
{
    private readonly ISqliteWasmDbContextFactory<StapleSourceContext> _dbContextFactory;

    public FetchBellFromDb(ISqliteWasmDbContextFactory<StapleSourceContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public async Task<List<BellSource>> GetBellSourcesAsync()
    {
        using var ctx = await _dbContextFactory.CreateDbContextAsync();
        if (!ctx.BellSources.Any())
        {
            await InsertBellSourcesAsync();
        }
        return ctx.BellSources.ToList();
    }

    public async Task<BellSource> InsertBellSourcesAsync()
    {
        try
        {
            using var ctx = await _dbContextFactory.CreateDbContextAsync();
            var newBell = new BellSource()
            {
                Id = ctx.BellSources.Any() ? (ctx.BellSources.Max(x => x.Id) + 1) : 1,
                Phone = ((long)new Random().Next(0, 100000) * (long)new Random().Next(0, 100000)).ToString().PadLeft(10, '0'),
                Amount = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 8),
                Comment = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 8),
                CommissionDetails = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 8),
                CustomerName = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 8),
                IMEI = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 8),
                LOB = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 8),
                OrderNumber = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 8),
                TransactionDate = DateTime.Now.AddDays(new Random().Next(-100000, 0)).ToShortDateString(),
            };
            ctx.BellSources.Add(newBell);
            await ctx.SaveChangesAsync();
            return newBell;
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}