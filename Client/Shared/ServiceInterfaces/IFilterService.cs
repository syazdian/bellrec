namespace Bell.Reconciliation.Frontend.Shared.ServiceInterfaces;

public interface IFilterService
{
    public FilterItemsDisplay GetFilterItems();

    public string GetFilterJson();

    public Task<string> GetHello();
}