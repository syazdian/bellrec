namespace Bell.Reconciliation.Frontend.Shared.ServiceInterfaces;

public interface ILocalDbRepository
{
    Task<List<BellSourceDto>> GetBellSourceCellPhoneFromLocalDb();
    Task<List<StaplesSourceDto>> GetStapleSourceCellPhoneFromLocalDb();
    Task<List<CompareBellStapleCellPhone>> GetBellStapleCompareCellPhoneFromLocalDb(); 
    Task<IEnumerable<BellSourceDto>> GetBellSourceNonCellPhoneFromLocalDb();
    Task<List<StaplesSourceDto>> GetStapleSourceNonCellPhoneFromLocalDb();
    Task<List<CompareBellStapleNonCellPhone>> GetBellStapleCompareNonCellPhoneFromLocalDb();

    Task<int> InsertDataToLocalDbAsync(BellStaplesSourceDto bellStaplesSources);

    Task InsertBellSourceToLocalDbAsync(List<BellSourceDto> bellSourceDtos);

    Task InsertStaplesToLocalDbAsync(List<StaplesSourceDto> staplesSourceDtos);

    Task<bool> LocalDbExist();
}