using Microsoft.Extensions.Configuration;

namespace Bell.Reconciliation.Frontend.Web.Services;

public class FetchData : IFetchData
{
    private readonly HttpClient _httpClient;
    private readonly ILocalDbRepository _localDb;
    private const int packageSize = 1000;
    private readonly string baseAddress;

    //TODO: THIS IS FOR DEV IT SHOULD BE BELL AND STAPLES COUNT
    private const int maximumDownload = 1000;

    public FetchData(HttpClient httpClient, ILocalDbRepository localDb, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _localDb = localDb;
        baseAddress = configuration["baseaddress"];
    }

    public async Task FetchDataFromServerDb()
    {
        try
        {
            await _localDb.PurgeTables();
            var dbcount = await _httpClient.GetFromJsonAsync<BellStapleCountDto>($"{baseAddress}/api/SyncData/FetchCountServerDatabase");
            if (dbcount is null || dbcount.StaplesCount == 0 || dbcount.BellCount == 0) throw new ArgumentNullException(nameof(dbcount));
            int startBellCount = 1;
            int lastBellCount = packageSize;

            int startStapleCount = 1;
            int lastStapleCount = packageSize;
            do
            {
                var staplesList = await _httpClient.GetFromJsonAsync<List<StaplesSourceDto>>($"{baseAddress}/api/SyncData/GetStaplesSourceItems/{startStapleCount}/{lastStapleCount}");

                if (staplesList is not null)
                    await _localDb.InsertStaplesToLocalDbAsync(staplesList);
                else throw new ArgumentNullException(nameof(staplesList));
                startStapleCount = lastStapleCount + 1;
                lastStapleCount = lastStapleCount + packageSize;
                if (lastStapleCount > dbcount.StaplesCount)
                    lastStapleCount = dbcount.StaplesCount;
            } while (startStapleCount <= dbcount.StaplesCount);

            do
            {
                var bellList = await _httpClient.GetFromJsonAsync<List<BellSourceDto>>($"{baseAddress}/api/SyncData/GetBellSourceitems/");
                if (bellList is not null)
                    await _localDb.InsertBellSourceToLocalDbAsync(bellList);
                else throw new ArgumentNullException(nameof(bellList));
                startBellCount = lastBellCount + 1;
                lastBellCount = lastBellCount + packageSize;
                if (lastBellCount > dbcount.BellCount)
                    lastBellCount = dbcount.BellCount;
            } while (startBellCount <= dbcount.BellCount);

            SyncLogsDto syncLogsDto = new SyncLogsDto()
            {
                Success = true,
                EndSync = DateTime.UtcNow,
                StartSync = DateTime.UtcNow,
            };
            await _localDb.InsertSyncLog(syncLogsDto);
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
            var res = await _httpClient.GetStringAsync($"{baseAddress}/api/syncData/GenerateServerDb/1000/y/10");

            if (res is "Done") { }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}