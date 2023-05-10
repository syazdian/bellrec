namespace Bell.Reconciliation.Frontend.Shared.ServiceInterfaces;

public interface ILocalDbRepository
{
    Task<List<BellSourceDto>> GetBellSourceFromLocalDb();

    Task<List<StaplesSourceDto>> GetStapleSourceFromLocalDb();

    Task<int> InsertDataToLocalDbAsync(BellStaplesSourceDto bellStaplesSources);
}