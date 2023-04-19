namespace Bell.Reconciliation.App.Client.Services.Interfaces
{
    public interface IFilterService
    {
        public Task<FilterItems> GetFilterItems();
    }
}