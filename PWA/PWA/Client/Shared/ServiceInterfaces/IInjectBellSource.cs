using Bell.Reconciliation.Common.Models;

namespace Bell.Reconciliation.Frontend.Shared.ServiceInterfaces;

public interface IInjectBellSource
{
    public Task<List<BellSource>> GetBellSourcesAsync();
    public Task<BellSource> InsertBellSourcesAsync();
}
