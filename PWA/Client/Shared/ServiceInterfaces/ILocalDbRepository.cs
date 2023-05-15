namespace Bell.Reconciliation.Frontend.Shared.ServiceInterfaces;

public interface ILocalDbRepository
{
    Task<List<BellSourceDto>> GetBellSourceFromLocalDb();

    Task<List<StaplesSourceDto>> GetStapleSourceFromLocalDb();

    Task<int> InsertDataToLocalDbAsync(BellStaplesSourceDto bellStaplesSources);

    Task InsertBellSourceToLocalDbAsync(List<BellSourceDto> bellSourceDtos);

    Task InsertStaplesToLocalDbAsync(List<StaplesSourceDto> staplesSourceDtos);

    Task<bool> LocalDbExist();
}