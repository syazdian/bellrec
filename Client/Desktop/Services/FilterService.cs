using Bell.Reconciliation.Common.Models.Domain;
using Bell.Reconciliation.Common.Utilities;
using Bell.Reconciliation.Frontend.Shared.ServiceInterfaces;
using System.Net.Http;
using System.Reflection;
using System.Text.Json;

namespace Bell.Reconciliation.Frontend.Desktop.Services;

public class FilterService : IFilterService
{
    public FilterService()
    {
    }

    public async Task<FilterItems> GetFilterItems()
    {
        try
        {
            // var dir = Path.GetDirectoryName(typeof(FilterService).Assembly.Location)!;
            //var dir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;
            //var response = File.ReadAllText(Path.Combine(dir, "wwwroot/Assets", "filteritems.txt"));

            //string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            //path = Path.Combine(path, @"..\..\..\Data", @"filteritems.txt");
            var response = File.ReadAllText(@"c:\Staples\filteritems.txt");

            FilterItems filterItems = new FilterItems();

            if (!string.IsNullOrEmpty(response))
            {
                filterItems = response.JsonDeserialize<FilterItems>();
            }

            return filterItems;
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
            //TODO What code must be implemented?
            return string.Empty;

            //var response = await new HttpClient().GetStringAsync("https://localhost:7131/api/FilterValue/GetFilterItems");
            //var response = await _httpClient.GetFromJsonAsync<FilterItems>($"/api/FilterValue/GetFilterItems");
            //var response = await _httpClient.GetFromJsonAsync<string>($"https://dev.tools.staples.ca/BellServices/Reconciliation/api/FilterValue/GetFilterItems");
            //return JsonSerializer.Serialize(response);
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}