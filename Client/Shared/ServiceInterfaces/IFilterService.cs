namespace Bell.Reconciliation.Frontend.Shared.ServiceInterfaces;

public interface IFilterService
{
    public FilterItemsDisplay GetFilterItems();

    public Task<string> GetHello();
}