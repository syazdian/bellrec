using Bell.Reconciliation.Common.Models.Domain;
using Bell.Reconciliation.Common.Utilities;
using Bell.Reconciliation.Frontend.Shared.ServiceInterfaces;
using System.Net.Http.Json;
using System.Reflection;
using System.Text.Json;

namespace Bell.Reconciliation.Frontend.Web.Services;

public class FilterService : IFilterService
{
    private readonly HttpClient _httpClient;
    private string baseurl;

    public FilterService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<FilterItems> GetFilterItems()
    {
        try
        {
            //var response = await new HttpClient().GetStringAsync("https://localhost:7131/api/FilterValue/GetFilterItems");
            // var response = await _httpClient.GetFromJsonAsync<FilterItems>($"/api/FilterValue/GetFilterItems");
            var response = await _httpClient.GetFromJsonAsync<FilterItems>($"https://dev.tools.staples.ca/BellServices/Reconciliation/api/FilterValue/GetFilterItems");
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
            //var response = await new HttpClient().GetStringAsync("https://localhost:7131/api/FilterValue/GetFilterItems");
            // var response = await _httpClient.GetFromJsonAsync<FilterItems>($"/api/FilterValue/GetFilterItems");
            var response = await _httpClient.GetFromJsonAsync<string>($"https://dev.tools.staples.ca/BellServices/Reconciliation/api/FilterValue/GetFilterItems");
            return JsonSerializer.Serialize(response);
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}