using Bell.Reconciliation.Common.Models.Domain;
using Bell.Reconciliation.Common.Utilities;
using Bell.Reconciliation.Frontend.Shared.ServiceInterfaces;
using System.Reflection;

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
}