using Bell.Reconciliation.Common.Models;

namespace Bell.Reconciliation.Frontend.Shared.ServiceInterfaces;

public interface ISyncData
{
    public Task FetchData();

    public Task UpsertData();

    public Task<List<BellSource>> GetDataFromLocalDb();
}