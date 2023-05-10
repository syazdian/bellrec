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
                //var bellStaplesSource = await _httpClient.GetFromJsonAsync<BellStaplesSource>($"/api/SyncData/GetBellSourceitems/{lastId}");
                var bellStaplesSource = await new HttpClient().GetFromJsonAsync<BellStaplesSource>($"https://localhost:7131/api/syncdata/getbellsourceitems/{lastId}");
                if (bellStaplesSource is not null)
                    lastId = await InsertDataToDbAsync(bellStaplesSource);
                else
                    return;
            } while (lastId < 300);
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task GenerateDataInServerDb()
    {
        try
        {
            var res = await _httpClient.GetStringAsync($"/api/syncData/FillSqlite/1000/y/10");

            if (res is "Done") { }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task<int> InsertDataToDbAsync(BellStaplesSource bellStaplesSources)
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