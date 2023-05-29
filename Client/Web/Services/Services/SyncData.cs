namespace Bell.Reconciliation.Frontend.Web.Services;

public class SyncData : ISyncData
{
    private readonly HttpClient _httpClient;
    private readonly ILocalDbRepository _localDb;
    private const int packageSize = 1000;

    //TODO: THIS IS FOR DEV IT SHOULD BE BELL AND STAPLES COUNT
    private const int maximumDownload = 1000;

    public SyncData(HttpClient httpClient, ILocalDbRepository localDb)
    {
        _httpClient = httpClient;
        _localDb = localDb;
    }

    public async Task FetchDataFromServerDb()
    {
        try
        {
            await _localDb.PurgeTables();
            var dbcount = await _httpClient.GetFromJsonAsync<BellStapleCountDto>($"/api/SyncData/FetchCountServerDatabase");
            if (dbcount is null || dbcount.StaplesCount == 0 || dbcount.BellCount == 0) throw new ArgumentNullException(nameof(dbcount));
            int startBellCount = 1;
            int lastBellCount = packageSize;
            do
            {
                var bellList = await _httpClient.GetFromJsonAsync<List<BellSourceDto>>($"/api/SyncData/GetBellSourceitems/{startBellCount}/{lastBellCount}");
                if (bellList is not null)
                    await _localDb.InsertBellSourceToLocalDbAsync(bellList);
                else throw new ArgumentNullException(nameof(bellList));
                startBellCount = lastBellCount + 1;
                lastBellCount = lastBellCount + packageSize;
            } while (lastBellCount <= maximumDownload);// dbcount.BellCount);
            int startStapleCount = 1;
            int lastStapleCount = packageSize;
            do
            {
                var staplesList = await _httpClient.GetFromJsonAsync<List<StaplesSourceDto>>($"/api/SyncData/GetStaplesSourceItems/{startStapleCount}/{lastStapleCount}");
                if (staplesList is not null)
                    await _localDb.InsertStaplesToLocalDbAsync(staplesList);
                else throw new ArgumentNullException(nameof(staplesList));
                startStapleCount = lastStapleCount + 1;
                lastStapleCount = lastStapleCount + packageSize;
            } while (lastStapleCount <= maximumDownload);// dbcount.StaplesCount);
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
            var res = await _httpClient.GetStringAsync($"/api/syncData/GenerateServerDb/1000/y/10");

            if (res is "Done") { }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public Task UpsertDataInServerDb()
    {
        throw new NotImplementedException();
    }
}