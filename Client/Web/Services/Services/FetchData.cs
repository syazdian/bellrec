using Microsoft.Extensions.Configuration;

namespace Bell.Reconciliation.Frontend.Web.Services;

public class FetchData : IFetchData
{
    private readonly HttpClient _httpClient;
    //private readonly IHttpClientFactory _ClientFactory;

    private readonly ILocalDbRepository _localDb;

    //private readonly IConfiguration _configuration;
    private const int packageSize = 1000;

    private readonly string baseAddress;

    //TODO: THIS IS FOR DEV IT SHOULD BE BELL AND STAPLES COUNT
    private const int maximumDownload = 1000;

    public FetchData(HttpClient httpClient, ILocalDbRepository localDb, IConfiguration configuration)
    {
        _httpClient = httpClient;
        //_ClientFactory = ClientFactory;
        _localDb = localDb;
        //_configuration = configuration;
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
                var res = staplesList.Where(x => x.OrderNumber == 83851628726).FirstOrDefault();

                if (staplesList is not null)
                    await _localDb.InsertStaplesToLocalDbAsync(staplesList);
                else throw new ArgumentNullException(nameof(staplesList));
                startStapleCount = lastStapleCount + 1;
                lastStapleCount = lastStapleCount + packageSize;
                if (lastStapleCount > dbcount.StaplesCount)
                    lastStapleCount = dbcount.StaplesCount;
            } while (startStapleCount <= maximumDownload);// dbcount.StaplesCount);

            //var _httpClient = _ClientFactory.CreateClient();
            do
            {
                var bellList = await _httpClient.GetFromJsonAsync<List<BellSourceDto>>($"{baseAddress}/api/SyncData/GetBellSourceitems/{startBellCount}/{lastBellCount}");
                if (bellList is not null)
                    await _localDb.InsertBellSourceToLocalDbAsync(bellList);
                else throw new ArgumentNullException(nameof(bellList));
                startBellCount = lastBellCount + 1;
                lastBellCount = lastBellCount + packageSize;
                if (lastBellCount > dbcount.BellCount)
                    lastBellCount = dbcount.BellCount;
            } while (startBellCount <= maximumDownload);// dbcount.BellCount);

            SyncLogsDto syncLogsDto = new SyncLogsDto()
            {
                Success = true,
                EndSync = DateTime.Now,
                StartSync = DateTime.Now,
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
            //var _httpClient = _ClientFactory.CreateClient();
            var res = await _httpClient.GetStringAsync($"{baseAddress}/api/syncData/GenerateServerDb/1000/y/10");

            if (res is "Done") { }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}