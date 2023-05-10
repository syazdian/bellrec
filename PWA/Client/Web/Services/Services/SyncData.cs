namespace Bell.Reconciliation.Frontend.Web.Services;

public class SyncData : ISyncData
{
    private readonly HttpClient _httpClient;
    private readonly ILocalDbRepository _localDb;

    public SyncData(HttpClient httpClient, ILocalDbRepository localDb)
    {
        _httpClient = httpClient;
        _localDb = localDb;
    }

    public async Task BellSourceGenerateFromMemory()
    {
        try
        {
            int lastId = 1;
            do
            {
                //var bellList = await _httpClient.GetFromJsonAsync<List<BellSource>>($"/api/SyncData/GetBellSourceitems/{lastId}");
                var bellList = await new HttpClient().GetFromJsonAsync<BellSourceDto[]>($"https://localhost:7131/api/syncdata/GetBellSourceitems/{lastId}");
                if (bellList is not null)
                    lastId = 1;// await InsertDataToDbAsync(bellList.ToList());
                else
                    return;
            } while (lastId < 100000);
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task FetchDataFromServerDb()
    {
        try
        {
            var bellStaplesSource = await _httpClient.GetFromJsonAsync<BellStaplesSourceDto>($"/api/SyncData/FetchFromDatabase");
            ArgumentNullException.ThrowIfNull(bellStaplesSource);
            await _localDb.InsertDataToLocalDbAsync(bellStaplesSource);
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

    public Task UpsertDataInServerDb()
    {
        throw new NotImplementedException();
    }
}