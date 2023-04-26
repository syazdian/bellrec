using Bell.Reconciliation.Common.Models.Domain;
using Bell.Reconciliation.Common.Utilities;
using Bell.Reconciliation.Frontend.Shared.ServiceInterfaces;

namespace Bell.Reconciliation.Frontend.Web.Services;

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
            var response = await httpClient.GetStringAsync("https://localhost:7131/api/FilterValue/GetFilterItems");
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