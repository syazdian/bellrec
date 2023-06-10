using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;

namespace Bell.Reconciliation.Frontend.Web.Services.Services;

public class SyncData : ISyncData
{
    private readonly HttpClient _httpClient;
    private readonly ILocalDbRepository _localDb;
    private readonly string baseAddress;

    public SyncData(HttpClient httpClient, ILocalDbRepository localDb, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _localDb = localDb;
        baseAddress = configuration["baseaddress"];
    }

    public async Task UpdateLocalDbWithNewChanges()
    {
        throw new NotImplementedException();
    }

    public async Task UpdateChangesToServerDb()
    {
        var notSyncedBellSources = await _localDb.GetNotSyncedUpdatedBellSource();
        var notSyncedStapleSources = await _localDb.GetNotSyncedUpdatedStapleSource();

        if (notSyncedBellSources.Count + notSyncedStapleSources.Count > 0)
        {
            SyncLogsDto syncLogsDto = new SyncLogsDto { StartSync = DateTime.UtcNow };
            try
            {
                if (notSyncedStapleSources.Count > 0)
                {
                    await _httpClient.PostAsJsonAsync($"/api/SyncData/SyncChangesStaple", notSyncedStapleSources);
                }

                if (notSyncedBellSources.Count > 0)
                {
                    await _httpClient.PostAsJsonAsync($"/api/SyncData/SyncChangesBell", notSyncedBellSources);
                }
                syncLogsDto.Success = true;
            }
            catch (Exception ex)
            {
                syncLogsDto.Success = false;
                throw;
            }
            finally
            {
                syncLogsDto.EndSync = DateTime.UtcNow;
                await _localDb.InsertSyncLog(syncLogsDto);
            }
        }
    }
}