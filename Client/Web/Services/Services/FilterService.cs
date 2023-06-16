using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Text.Json;

namespace Bell.Reconciliation.Frontend.Web.Services;

public class FilterService : IFilterService
{
    private readonly HttpClient _httpClient;
    private readonly string baseAddress;
    private readonly string filterJson;
    private readonly FilterItemsDisplay _filterItem;

    public FilterService(HttpClient httpClient, IConfiguration configuration, FilterItemsDisplay filterItem)
    {
        _httpClient = httpClient;
        _filterItem = filterItem;

        baseAddress = configuration["baseaddress"];
        filterJson = configuration.["FilterItems:Brand"];
    }

    public FilterItemsDisplay GetFilterItems()
    {
        try
        {
            return _filterItem;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public string GetFilterJson()
    {
        return filterJson;
    }

    public async Task<string> GetHello()
    {
        try
        {
            var url = $"{baseAddress}/api/FilterValue/GetFilterItems";
            var response = await _httpClient.GetFromJsonAsync<FilterItemsDisplay>(url);
            return JsonSerializer.Serialize(response);
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}