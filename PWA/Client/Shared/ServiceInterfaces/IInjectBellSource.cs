namespace Bell.Reconciliation.Frontend.Shared.ServiceInterfaces;

public interface IInjectBellSource
{
    public Task<List<BellSourceDto>> GetBellSourcesAsync();

    public Task<BellSourceDto> InsertBellSourcesAsync();
}