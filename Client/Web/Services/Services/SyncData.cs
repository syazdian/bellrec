using Bell.Reconciliation.Frontend.Web.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;

namespace Bell.Reconciliation.Frontend.Web.Services.Services;

public class SyncData : ISyncData
{
    private readonly HttpClient _httpClient;
    private readonly ILocalDbRepository _localDb;
    private readonly string baseAddress;

    private DateTime latesSyncDate;
    private IStateContainer _stateContainer;

    public SyncData(HttpClient httpClient, ILocalDbRepository localDb, IConfiguration configuration, IStateContainer stateContainer)
    {
        _httpClient = httpClient;
        _localDb = localDb;
        _stateContainer = stateContainer;
        baseAddress = configuration["baseaddress"];
    }

    public async Task StartSyncData()
    {
        await GetLatestSyncDate();
        await UpdateLocalDbWithNewChangesFromServer();
        await UpdateChangesToServerDb();
    }

    public async Task UpdateLocalDbWithNewChangesFromServer()
    {
        try
        {
            var formattedDateToSent = latesSyncDate.ToString("yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            var currentUser = _stateContainer.User;
            var staplesList = await _httpClient.GetFromJsonAsync<List<StaplesSourceDto>>
                ($"{baseAddress}/api/SyncData/GetLatestChangedStaplesSourceItemsByDate/{formattedDateToSent}/{currentUser}");
            var bellList = await _httpClient.GetFromJsonAsync<List<BellSourceDto>>
                ($"{baseAddress}/api/SyncData/GetLatestChangedBellSourceItemsByDate/{formattedDateToSent}/{currentUser}");
            await _localDb.UpdateLatestDownloadedBellAndStaplesToLocalDb(staplesList, bellList);
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task GetLatestSyncDate()
    {
        latesSyncDate = await _localDb.GetLatestSyncDate();
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
                    await _httpClient.PostAsJsonAsync($"{baseAddress}/api/SyncData/SyncChangesStaple", notSyncedStapleSources);
                }

                if (notSyncedBellSources.Count > 0)
                {
                    await _httpClient.PostAsJsonAsync($"{baseAddress}/api/SyncData/SyncChangesBell", notSyncedBellSources);
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