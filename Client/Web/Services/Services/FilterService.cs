using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Text.Json;

namespace Bell.Reconciliation.Frontend.Web.Services;

public class FilterService : IFilterService
{
    private readonly HttpClient _httpClient;
    // private readonly IHttpClientFactory _ClientFactory;

    private readonly string baseAddress;

    public FilterService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;

        baseAddress = configuration["baseaddress"];

        //_ClientFactory = ClientFactory;
    }

    public async Task<FilterItems> GetFilterItems()
    {
        try
        {
            var response = await _httpClient.GetFromJsonAsync<FilterItems>($"{baseAddress}/api/FilterValue/GetFilterItems");
            return response;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task<string> GetHello()
    {
        try
        {
            var url = $"{baseAddress}/api/FilterValue/GetFilterItems";
            var response = await _httpClient.GetFromJsonAsync<FilterItems>(url);
            return JsonSerializer.Serialize(response);
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}