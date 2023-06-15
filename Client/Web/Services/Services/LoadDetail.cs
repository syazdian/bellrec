using Microsoft.Extensions.Configuration;

namespace Bell.Reconciliation.Frontend.Web.Services;

public class LoadDetail : ILoadDetail
{
    private readonly HttpClient _httpClient;
    private readonly ILocalDbRepository _localDb;
    private readonly string baseAddress;

    public LoadDetail(HttpClient httpClient, ILocalDbRepository localDb, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _localDb = localDb;
        baseAddress = configuration["baseaddress"];
    }

    public Task<string> GetDeatilByPhoneNumber(string phone)
    {
        throw new NotImplementedException();
        //var dbcount = await _httpClient.GetFromJsonAsync<BellStapleCountDto>($"{baseAddress}/api/FetchData/FetchCountServerDatabase");
    }

    public Task<string> GetDeatilByImei(string imei)
    {
        throw new NotImplementedException();
    }

    public Task<string> GetDeatilByOrderNumber(string ordernumber)
    {
        throw new NotImplementedException();
    }
}