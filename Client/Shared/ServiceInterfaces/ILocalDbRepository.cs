using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Bell.Reconciliation.Frontend.Shared.ServiceInterfaces;

public interface ILocalDbRepository
{
    Task<List<BellSourceDto>> GetBellSourceCellPhoneFromLocalDb(FilterItemDto filterItemDto);

    Task<List<StaplesSourceDto>> GetStapleSourceCellPhoneFromLocalDb(FilterItemDto filterItemDto);

    Task<List<CompareBellStapleCellPhone>> GetBellStapleCompareCellPhoneFromLocalDb(FilterItemDto filterItemDto);

    Task<List<BellSourceDto>> GetBellSourceNonCellPhoneFromLocalDb(FilterItemDto filterItemDto);

    Task<List<StaplesSourceDto>> GetStapleSourceNonCellPhoneFromLocalDb(FilterItemDto filterItemDto);

    Task<List<CompareBellStapleNonCellPhone>> GetBellStapleCompareNonCellPhoneFromLocalDb(FilterItemDto filterItemDto);

    Task InsertSyncLog(SyncLogsDto syncLogsDto);

    Task<int> StartSyncLog();

    Task FinishedSyncLog(int id, bool success);

    Task<List<BellSourceDto>> GetNotSyncedUpdatedBellSource();

    Task<List<StaplesSourceDto>> GetNotSyncedUpdatedStapleSource();

    Task<int> InsertDataToLocalDbAsync(BellStaplesSourceDto bellStaplesSources);

    Task InsertBellSourceToLocalDbAsync(List<BellSourceDto> bellSourceDtos);

    Task InsertStaplesToLocalDbAsync(List<StaplesSourceDto> staplesSourceDtos);

    Task<bool> UpdateBellSource(BellSourceDto bellSourceDto);

    Task<bool> UpdateStapleSource(StaplesSourceDto staplesSourceDto);

    Task<EntityEntry<BellSourceDto>> GetBellSourceEntry(BellSourceDto record);

    Task<bool> UpdateBellSource(long Id, string Comment);

    Task<bool> UpdateStapleSource(long Id, string Comment);

    Task<EntityEntry<StaplesSourceDto>> GetStapleSourceEntry(StaplesSourceDto record);

    Task<bool> LocalDbExist();

    Task<bool> PurgeTables();
}