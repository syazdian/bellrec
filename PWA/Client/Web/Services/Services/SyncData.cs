using Bell.Reconciliation.Common.Models;
using Bell.Reconciliation.Common.Models.Domain;
using Bell.Reconciliation.Common.Utilities;
using Bell.Reconciliation.Frontend.Shared.ServiceInterfaces;
using Bell.Reconciliation.Frontend.Web.Database;
using Microsoft.EntityFrameworkCore;
using SqliteWasmHelper;
using System.Collections.Generic;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace Bell.Reconciliation.Frontend.Web.Services;

public class SyncData : ISyncData
{
    private readonly ISqliteWasmDbContextFactory<StapleSourceContext> _dbContextFactory;
    private readonly HttpClient _httpClient;

    public SyncData(ISqliteWasmDbContextFactory<StapleSourceContext> dbContextFactory, HttpClient httpClient)
    {
        _dbContextFactory = dbContextFactory;
        _httpClient = httpClient;
    }

    public async Task FetchData()
    {
        try
        {
            int lastId = 1;
            do
            {
                //var bellList = await _httpClient.GetFromJsonAsync<List<BellSource>>($"/api/SyncData/GetBellSourceitems/{lastId}");
                var bellList = await new HttpClient().GetFromJsonAsync<BellSource[]>($"https://localhost:7131/api/syncdata/getbellsourceitems/{lastId}");
                if (bellList is not null)
                    lastId = await InsertDataToDbAsync(bellList.ToList());
                else
                    return;
            } while (lastId < 100000);
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task<int> InsertDataToDbAsync(List<BellSource> bellSources)
    {
        try
        {
            using var ctx = await _dbContextFactory.CreateDbContextAsync();

            await ctx.BellSources.AddRangeAsync(bellSources);
            await ctx.SaveChangesAsync();
            return bellSources.Max(x => x.Id);
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public Task UpsertData()
    {
        throw new NotImplementedException();
    }

    public async Task<List<BellSource>> GetDataFromLocalDb()
    {
        using var ctx = await _dbContextFactory.CreateDbContextAsync();
        List<BellSource> bellSources = await ctx.BellSources.ToListAsync();
        return bellSources;
    }
}