﻿using Microsoft.Extensions.Configuration;
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
        var notSyncedBellSources = _localDb.GetNotSyncedUpdatedBellSource().Result;
        var notSyncedStapleSources = _localDb.GetNotSyncedUpdatedStapleSource().Result;

        if (notSyncedBellSources.Count + notSyncedStapleSources.Count > 0)
        {
            SyncLogsDto syncLogsDto = new SyncLogsDto();
            syncLogsDto.StartSync = DateTime.Now;
            try
            {
                if (notSyncedStapleSources.Count > 0)
                {
                    var response = await _httpClient.PostAsJsonAsync($"{baseAddress}/api/SyncData/SyncChangesStaple", notSyncedStapleSources);
                }

                if (notSyncedBellSources.Count > 0)
                {
                    var response = await _httpClient.PostAsJsonAsync($"{baseAddress}/api/SyncData/SyncChangesBell", notSyncedBellSources);
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
                syncLogsDto.EndSync = DateTime.Now;
                await _localDb.InsertSyncLog(syncLogsDto);
            }
        }
    }
}