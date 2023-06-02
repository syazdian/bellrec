using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Bell.Reconciliation.Frontend.Shared.ServiceInterfaces;

public interface ILocalDbRepository
{
    Task<List<BellSourceDto>> GetBellSourceCellPhoneFromLocalDb();

    Task<List<StaplesSourceDto>> GetStapleSourceCellPhoneFromLocalDb();

    Task<List<CompareBellStapleCellPhone>> GetBellStapleCompareCellPhoneFromLocalDb();

    Task<List<BellSourceDto>> GetBellSourceNonCellPhoneFromLocalDb();

    Task<List<StaplesSourceDto>> GetStapleSourceNonCellPhoneFromLocalDb();

    Task<List<CompareBellStapleNonCellPhone>> GetBellStapleCompareNonCellPhoneFromLocalDb();

    Task<int> InsertDataToLocalDbAsync(BellStaplesSourceDto bellStaplesSources);

    Task InsertBellSourceToLocalDbAsync(List<BellSourceDto> bellSourceDtos);

    Task InsertStaplesToLocalDbAsync(List<StaplesSourceDto> staplesSourceDtos);

    Task<bool> UpdateBellSource(BellSourceDto bellSourceDto);
    
    Task<bool> UpdateStapleSource(StaplesSourceDto staplesSourceDto);

    Task<EntityEntry<BellSourceDto>> GetBellSourceEntry(BellSourceDto record);

    Task<EntityEntry<StaplesSourceDto>> GetStapleSourceEntry(StaplesSourceDto record);

    Task<bool> LocalDbExist();

    Task<bool> PurgeTables();
}