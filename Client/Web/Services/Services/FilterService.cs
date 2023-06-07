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
            //var dir = Path.GetDirectoryName(typeof(FilterService).Assembly.Location)!;
            //var response = File.ReadAllText(Path.Combine(dir, "Data", "filteritems.txt"));

            //var response = await new HttpClient().GetStringAsync("https://localhost:7131/api/FilterValue/GetFilterItems");
            //var baseaddress = _httpClient.BaseAddress;
            //  var client = _ClientFactory.CreateClient();
            var response = await _httpClient.GetFromJsonAsync<FilterItems>($"{baseAddress}/api/FilterValue/GetFilterItems");
            // var response = await _httpClient.GetFromJsonAsync<FilterItems>($"/api/FilterValue/GetFilterItems");
            //var response = await _httpClient.GetFromJsonAsync<FilterItems>($"https://dev.tools.staples.ca/BellServices/Reconciliation/api/FilterValue/GetFilterItems");
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
            //var client = _ClientFactory.CreateClient();
            var url = $"/api/FilterValue/GetFilterItems";
            //var response = await new HttpClient().GetStringAsync("https://localhost:7131/api/FilterValue/GetFilterItems");
            var response = await _httpClient.GetFromJsonAsync<FilterItems>(url);
            //var response = await _httpClient.GetFromJsonAsync<string>($"https://dev.tools.staples.ca/BellServices/Reconciliation/api/FilterValue/GetFilterItems");
            return JsonSerializer.Serialize(response);
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}