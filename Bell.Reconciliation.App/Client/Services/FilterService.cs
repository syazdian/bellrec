using Bell.Reconciliation.App.Client.Services.Interfaces;

namespace Bell.Reconciliation.App.Client.Services;

public class FilterService : IFilterService
{
    public FilterService()
    {
    }

    public async Task<FilterItems> GetFilterItems()
    {
        try
        {
            HttpClient httpClient = new HttpClient();
            var response = await httpClient.GetStringAsync("http://localhost:5131/api/FilterValue/GetFilterItems");
            //var response = await httpClient.GetStringAsync("/api/FilterValue/GetFilterItems");

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
}