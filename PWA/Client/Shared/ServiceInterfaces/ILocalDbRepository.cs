namespace Bell.Reconciliation.Frontend.Shared.ServiceInterfaces;

public interface ILocalDbRepository
{
    Task<List<BellSourceDto>> GetBellSourceFromLocalDb();

    Task<int> InsertDataToLocalDbAsync(BellStaplesSourceDto bellStaplesSources);
}