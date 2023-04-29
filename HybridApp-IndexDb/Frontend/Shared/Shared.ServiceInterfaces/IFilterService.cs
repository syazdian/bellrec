using Bell.Reconciliation.Common.Models.Domain;

namespace Bell.Reconciliation.Frontend.Shared.ServiceInterfaces;

public interface IFilterService
{
    public Task<FilterItems> GetFilterItems();
}