using Bell.Reconciliation.Common.Models.Enums;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Data;
using Bell.Reconciliation.Frontend.Web.Models;
using Bell.Reconciliation.Common.Models;

namespace Bell.Reconciliation.Frontend.Web.Services.Database;

public class LocalDbRepository : ILocalDbRepository
{
    private readonly ISqliteWasmDbContextFactory<StapleSourceContext> _dbContextFactory;

    private IStateContainer _stateContainer;

    public LocalDbRepository(ISqliteWasmDbContextFactory<StapleSourceContext> dbContextFactory, IStateContainer stateContainer)
    {
        _dbContextFactory = dbContextFactory;
        _stateContainer = stateContainer;
    }

    #region SYNC UPDATE DOWNLOAD TATEST CHANGES

    public async Task InsertSyncLog(SyncLogsDto syncLogsDto)
    {
        try
        {
            using var ctx = await _dbContextFactory.CreateDbContextAsync();
            ctx.SyncLogs.Add(syncLogsDto);
            await ctx.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task<DateTime> GetLatestSyncDate()
    {
        using var ctx = await _dbContextFactory.CreateDbContextAsync();

        var recentSyncDate = ctx.SyncLogs.Where(x => x.Success == true).Max(c => c.EndSync);
        return (DateTime)recentSyncDate;
    }

    public async Task UpdateLatestDownloadedBellAndStaplesToLocalDb(List<StaplesSourceDto> staples, List<BellSourceDto> bellSources)
    {
        try
        {
            using var ctx = await _dbContextFactory.CreateDbContextAsync();

            foreach (var bell in bellSources)
            {
                BellSourceDto? bellsourceInLocal = ctx.BellSources.Where(c => c.OrderNumber == bell.OrderNumber && c.RebateType == bell.RebateType).FirstOrDefault();
                if (bellsourceInLocal == null)
                {
                    ctx.BellSources.Add(bellsourceInLocal);
                    continue;
                }
                bellsourceInLocal.Comment = bell.Comment;
                bellsourceInLocal.MatchStatus = bell.MatchStatus;
                bellsourceInLocal.Reconciled = bell.Reconciled;
                bellsourceInLocal.ReconciledBy = bell.ReconciledBy;
                bellsourceInLocal.ReconciledDate = bell.ReconciledDate;
                bellsourceInLocal.UpdateDate = bell.UpdateDate;
                bellsourceInLocal.Imei = bell.Imei;
                bellsourceInLocal.Phone = bell.Phone;
                ctx.BellSources.Update(bellsourceInLocal);
            }

            foreach (var staple in staples)
            {
                StaplesSourceDto? staplesourceInLocal = ctx.StaplesSources.Where(c => c.OrderNumber == staple.OrderNumber && c.RebateType == staple.RebateType).FirstOrDefault();
                if (staplesourceInLocal == null)
                {
                    ctx.StaplesSources.Add(staplesourceInLocal);
                    continue;
                }
                staplesourceInLocal.Comment = staple.Comment;
                staplesourceInLocal.MatchStatus = staple.MatchStatus;
                staplesourceInLocal.Reconciled = staple.Reconciled;
                staplesourceInLocal.ReconciledBy = staple.ReconciledBy;
                staplesourceInLocal.ReconciledDate = staple.ReconciledDate;
                staplesourceInLocal.UpdateDate = staple.UpdateDate;
                staplesourceInLocal.Imei = staple.Imei;
                staplesourceInLocal.Phone = staple.Phone;
                ctx.StaplesSources.Update(staplesourceInLocal);
            }
            ctx.SaveChanges();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<List<BellSourceDto>> GetNotSyncedUpdatedBellSource()
    {
        try
        {
            using var ctx = await _dbContextFactory.CreateDbContextAsync();

            var recentSyncDate = ctx.SyncLogs.Where(x => x.Success == true).Max(c => c.EndSync);
            var query = ctx.BellSources.Where(c => c.ReconciledDate.HasValue && c.ReconciledDate > recentSyncDate).AsQueryable();
            List<BellSourceDto> bellSourceDtos = query.ToList();
            return bellSourceDtos;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task<List<StaplesSourceDto>> GetNotSyncedUpdatedStapleSource()
    {
        try
        {
            using var ctx = await _dbContextFactory.CreateDbContextAsync();

            var recentSyncDate = ctx.SyncLogs.Where(x => x.Success == true).Max(c => c.EndSync);
            var res = ctx.StaplesSources.Where(c => c.ReconciledDate.HasValue && c.ReconciledDate > recentSyncDate).ToList();
            List<StaplesSourceDto> staplesSourceDtos = res;
            return staplesSourceDtos;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    #endregion SYNC UPDATE DOWNLOAD TATEST CHANGES

    #region Insert Fetch Data to LocalDB

    public async Task InsertBellSourceToLocalDbAsync(List<BellSourceDto> bellSourceDtos)
    {
        try
        {
            using var ctx = await _dbContextFactory.CreateDbContextAsync();

            await ctx.BellSources.AddRangeAsync(bellSourceDtos);
            await ctx.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task InsertStaplesToLocalDbAsync(List<StaplesSourceDto> staplesSourceDtos)
    {
        try
        {
            using var ctx = await _dbContextFactory.CreateDbContextAsync();

            await ctx.StaplesSources.AddRangeAsync(staplesSourceDtos);
            await ctx.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task<int> InsertDataToLocalDbAsync(BellStaplesSourceDto bellStaplesSources)
    {
        try
        {
            using var ctx = await _dbContextFactory.CreateDbContextAsync();

            await ctx.BellSources.AddRangeAsync(bellStaplesSources.BellSources.ToList());
            await ctx.StaplesSources.AddRangeAsync(bellStaplesSources.StaplesSources.ToList());
            await ctx.SaveChangesAsync();
            return bellStaplesSources.BellSources.Max(x => x.Id);
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    #endregion Insert Fetch Data to LocalDB

    #region QUERY FOR UI DATAGRIDS

    public async Task<List<BellSourceDto>> GetBellSourceCellPhoneFromLocalDb(FilterItemDto filterItemDto)
    {
        try
        {
            using var ctx = await _dbContextFactory.CreateDbContextAsync();

            IQueryable<BellSourceDto> query = ctx.BellSources.AsQueryable();
            query = query.Where(b => b.SubLob == "Wireless" && !ctx.StaplesSources.Any(s => s.Phone == b.Phone || s.Imei == b.Imei));
            if (!string.IsNullOrEmpty(filterItemDto.RebateValue))
                query = query.Where(c => c.RebateType == filterItemDto.RebateValue);
            if (!string.IsNullOrEmpty(filterItemDto.Lob))
                query = query.Where(c => c.Lob == filterItemDto.Lob);
            if (!string.IsNullOrEmpty(filterItemDto.SubLob))
                query = query.Where(c => c.SubLob == filterItemDto.SubLob);
            if (filterItemDto.TransactionDateFrom.HasValue)
                query = query.Where(c => c.TransactionDate >= filterItemDto.TransactionDateFrom);
            if (filterItemDto.TransactionDateTo.HasValue)
                query = query.Where(c => c.TransactionDate <= filterItemDto.TransactionDateTo);
            List<BellSourceDto> bellSourceDtos = await query.OrderBy(s => s.Id).ToListAsync();
            return bellSourceDtos;
        }
        catch (Exception ex)
        {
            var f = ex.Message;
            return null;
        }
    }

    public async Task<List<StaplesSourceDto>> GetStapleSourceCellPhoneFromLocalDb(FilterItemDto filterItemDto)
    {
        using var ctx = await _dbContextFactory.CreateDbContextAsync();
        IQueryable<StaplesSourceDto> query = ctx.StaplesSources.AsQueryable();
        query = query.Where(s => s.SubLob == "Wireless" && !ctx.BellSources.Any(b => b.Phone == s.Phone || b.Imei == s.Imei));
        if (!string.IsNullOrEmpty(filterItemDto.RebateValue))
            query = query.Where(c => c.RebateType == filterItemDto.RebateValue);
        if (!string.IsNullOrEmpty(filterItemDto.Lob))
            query = query.Where(c => c.Lob == filterItemDto.Lob);
        if (!string.IsNullOrEmpty(filterItemDto.SubLob))
            query = query.Where(c => c.SubLob == filterItemDto.SubLob);
        if (!string.IsNullOrEmpty(filterItemDto.Location))
            query = query.Where(c => c.Location == filterItemDto.Location);
        if (!string.IsNullOrEmpty(filterItemDto.Brand))
            query = query.Where(c => c.Brand == filterItemDto.Brand);
        if (filterItemDto.TransactionDateFrom.HasValue)
            query = query.Where(c => c.TransactionDate >= filterItemDto.TransactionDateFrom);
        if (filterItemDto.TransactionDateTo.HasValue)
            query = query.Where(c => c.TransactionDate <= filterItemDto.TransactionDateTo);

        List<StaplesSourceDto> stpSourceDtos = await query.OrderBy(s => s.Id).ToListAsync();
        return stpSourceDtos;
    }

    //TODO: This should be optimized like other ones with IQueryable
    public async Task<List<CompareBellStapleCellPhone>> GetBellStapleCompareCellPhoneFromLocalDb(FilterItemDto filterItemDto)
    {
        try
        {
            using var ctx = await _dbContextFactory.CreateDbContextAsync();

            List<CompareBellStapleCellPhone> bellStaplesCompares
                = (from b in ctx.BellSources
                   join s in ctx.StaplesSources on b.Phone equals s.Phone
                   where s.SubLob == "Wireless" && b.SubLob == "Wireless"
                   && s.RebateType == b.RebateType &&
                   (string.IsNullOrEmpty(filterItemDto.RebateValue) || s.RebateType == filterItemDto.RebateValue) &&
                   (string.IsNullOrEmpty(filterItemDto.Lob) || s.Lob == filterItemDto.Lob) &&
                   (string.IsNullOrEmpty(filterItemDto.Lob) || b.Lob == filterItemDto.Lob) &&
                   (string.IsNullOrEmpty(filterItemDto.SubLob) || s.SubLob == filterItemDto.SubLob) &&
                   (string.IsNullOrEmpty(filterItemDto.SubLob) || b.SubLob == filterItemDto.SubLob) &&
                   (string.IsNullOrEmpty(filterItemDto.Location) || s.Location == filterItemDto.Location) &&
                   (string.IsNullOrEmpty(filterItemDto.Brand) || s.Brand == filterItemDto.Brand) &&
                   (string.IsNullOrEmpty(filterItemDto.RebateValue) || s.RebateType == filterItemDto.RebateValue) &&
                   (string.IsNullOrEmpty(filterItemDto.RebateValue) || b.RebateType == filterItemDto.RebateValue) &&
                   (!filterItemDto.TransactionDateFrom.HasValue || s.TransactionDate >= filterItemDto.TransactionDateFrom) &&
                   (!filterItemDto.TransactionDateFrom.HasValue || b.TransactionDate >= filterItemDto.TransactionDateFrom) &&
                   (!filterItemDto.TransactionDateTo.HasValue || s.TransactionDate <= filterItemDto.TransactionDateTo) &&
                   (!filterItemDto.TransactionDateTo.HasValue || b.TransactionDate <= filterItemDto.TransactionDateTo)
                   select new CompareBellStapleCellPhone
                   {
                       BPhone = b.Phone.ToString(),
                       BLob = b.Lob,
                       BSublob = b.SubLob,
                       BIMEI = b.Imei.ToString(),
                       BOrderNumber = b.OrderNumber.ToString(),
                       BAmount = b.Amount,
                       BComment = b.Comment.ToString(),
                       BTransactionDate = b.TransactionDate,
                       BCustomerName = b.CustomerName.ToString(),
                       BRebateType = b.RebateType.ToString(),
                       BReconciled = b.Reconciled,
                       BId = b.Id,

                       SPhone = s.Phone.ToString(),
                       SLob = s.Lob,
                       SSublob = s.SubLob,
                       SIMEI = s.Imei.ToString(),
                       SOrderNumber = s.OrderNumber.ToString(),
                       SAmount = s.Amount,
                       SComment = s.Comment.ToString(),
                       STransactionDate = s.TransactionDate,
                       SCustomerName = s.CustomerName.ToString(),
                       SRebateType = b.RebateType.ToString(),
                       SReconciled = b.Reconciled,
                       SId = s.Id,

                       MatchStatus = (s.Reconciled == true && b.Reconciled == true) ? MatchStatus.Reconciled :
                       ((s.Amount == b.Amount && s.OrderNumber == b.OrderNumber &&
                       s.TransactionDate == b.TransactionDate && s.CustomerName == b.CustomerName && s.Imei == b.Imei && s.Phone == s.Phone) ? MatchStatus.Match : MatchStatus.Missmatch)
                   }).ToList()
                .Concat(from b in ctx.BellSources
                        join s in ctx.StaplesSources on b.Imei equals s.Imei
                        where s.SubLob == "Wireless" && b.SubLob == "Wireless"
                        && s.Phone != b.Phone
                        && s.RebateType == b.RebateType &&
                        (string.IsNullOrEmpty(filterItemDto.RebateValue) || s.RebateType == filterItemDto.RebateValue) &&
                        (string.IsNullOrEmpty(filterItemDto.Lob) || s.Lob == filterItemDto.Lob) &&
                        (string.IsNullOrEmpty(filterItemDto.Lob) || b.Lob == filterItemDto.Lob) &&
                        (string.IsNullOrEmpty(filterItemDto.SubLob) || s.SubLob == filterItemDto.SubLob) &&
                        (string.IsNullOrEmpty(filterItemDto.SubLob) || b.SubLob == filterItemDto.SubLob) &&
                        (string.IsNullOrEmpty(filterItemDto.Location) || s.Location == filterItemDto.Location) &&
                        (string.IsNullOrEmpty(filterItemDto.Brand) || s.Brand == filterItemDto.Brand) &&
                        (string.IsNullOrEmpty(filterItemDto.RebateValue) || s.RebateType == filterItemDto.RebateValue) &&
                        (string.IsNullOrEmpty(filterItemDto.RebateValue) || b.RebateType == filterItemDto.RebateValue) &&
                        (!filterItemDto.TransactionDateFrom.HasValue || s.TransactionDate >= filterItemDto.TransactionDateFrom) &&
                        (!filterItemDto.TransactionDateFrom.HasValue || b.TransactionDate >= filterItemDto.TransactionDateFrom) &&
                        (!filterItemDto.TransactionDateTo.HasValue || s.TransactionDate <= filterItemDto.TransactionDateTo) &&
                        (!filterItemDto.TransactionDateTo.HasValue || b.TransactionDate <= filterItemDto.TransactionDateTo)
                        select new CompareBellStapleCellPhone
                        {
                            BPhone = b.Phone.ToString(),
                            BLob = b.Lob,
                            BSublob = b.SubLob,
                            BIMEI = b.Imei.ToString(),
                            BOrderNumber = b.OrderNumber.ToString(),
                            BAmount = b.Amount,
                            BComment = b.Comment.ToString(),
                            BTransactionDate = b.TransactionDate,
                            BCustomerName = b.CustomerName.ToString(),
                            BRebateType = b.RebateType.ToString(),
                            BReconciled = b.Reconciled,
                            BId = b.Id,

                            SPhone = s.Phone.ToString(),
                            SLob = s.Lob,
                            SSublob = s.SubLob,
                            SIMEI = s.Imei.ToString(),
                            SOrderNumber = s.OrderNumber.ToString(),
                            SAmount = s.Amount,
                            SComment = s.Comment.ToString(),
                            STransactionDate = s.TransactionDate,
                            SCustomerName = s.CustomerName.ToString(),
                            SRebateType = b.RebateType.ToString(),
                            SReconciled = b.Reconciled,
                            SBrand = s.Brand,
                            SLocation = s.Location,
                            SId = s.Id,

                            MatchStatus = (s.Reconciled == true && b.Reconciled == true) ? MatchStatus.Reconciled :
                            ((s.Amount == b.Amount && s.OrderNumber == b.OrderNumber &&
                            s.TransactionDate == b.TransactionDate && s.CustomerName == b.CustomerName && s.Imei == b.Imei && s.Phone == s.Phone) ? MatchStatus.Match : MatchStatus.Missmatch)
                        })
                                  .OrderBy(x => x.SId)
                                  .ToList();

            return bellStaplesCompares;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public async Task<List<BellSourceDto>> GetBellSourceNonCellPhoneFromLocalDb(FilterItemDto filterItemDto)
    {
        using var ctx = await _dbContextFactory.CreateDbContextAsync();

        IQueryable<BellSourceDto> query = ctx.BellSources.AsQueryable();
        query = query.Where(b => b.SubLob != "Wireless" && !ctx.StaplesSources.Any(s => s.OrderNumber == b.OrderNumber && s.RebateType == b.RebateType));
        if (!string.IsNullOrEmpty(filterItemDto.RebateValue))
            query = query.Where(c => c.RebateType == filterItemDto.RebateValue);
        if (!string.IsNullOrEmpty(filterItemDto.Lob))
            query = query.Where(c => c.Lob == filterItemDto.Lob);
        if (!string.IsNullOrEmpty(filterItemDto.SubLob))
            query = query.Where(c => c.SubLob == filterItemDto.SubLob);
        if (filterItemDto.TransactionDateFrom.HasValue)
            query = query.Where(c => c.TransactionDate >= filterItemDto.TransactionDateFrom);
        if (filterItemDto.TransactionDateTo.HasValue)
            query = query.Where(c => c.TransactionDate <= filterItemDto.TransactionDateTo);
        List<BellSourceDto> bellSourceDtos = await query.OrderBy(s => s.Id).ToListAsync();
        return bellSourceDtos;
    }

    public async Task<List<StaplesSourceDto>> GetStapleSourceNonCellPhoneFromLocalDb(FilterItemDto filterItemDto)
    {
        using var ctx = await _dbContextFactory.CreateDbContextAsync();
        IQueryable<StaplesSourceDto> query = ctx.StaplesSources.AsQueryable();
        query = query.Where(s => s.SubLob != "Wireless" && !ctx.BellSources.Any(b => s.OrderNumber == b.OrderNumber && s.RebateType == b.RebateType));
        if (!string.IsNullOrEmpty(filterItemDto.RebateValue))
            query = query.Where(c => c.RebateType == filterItemDto.RebateValue);
        if (!string.IsNullOrEmpty(filterItemDto.Lob))
            query = query.Where(c => c.Lob == filterItemDto.Lob);
        if (!string.IsNullOrEmpty(filterItemDto.SubLob))
            query = query.Where(c => c.SubLob == filterItemDto.SubLob);
        if (!string.IsNullOrEmpty(filterItemDto.Location))
            query = query.Where(c => c.Location == filterItemDto.Location);
        if (!string.IsNullOrEmpty(filterItemDto.Brand))
            query = query.Where(c => c.Brand == filterItemDto.Brand);
        if (filterItemDto.TransactionDateFrom.HasValue)
            query = query.Where(c => c.TransactionDate >= filterItemDto.TransactionDateFrom);
        if (filterItemDto.TransactionDateTo.HasValue)
            query = query.Where(c => c.TransactionDate <= filterItemDto.TransactionDateTo);

        List<StaplesSourceDto> stpSourceDtos = await query.OrderBy(s => s.Id).ToListAsync();
        return stpSourceDtos;
    }

    //TODO: This should be optimized like other ones with IQueryable
    public async Task<List<CompareBellStapleNonCellPhone>> GetBellStapleCompareNonCellPhoneFromLocalDb(FilterItemDto filterItemDto)
    {
        try
        {
            using var ctx = await _dbContextFactory.CreateDbContextAsync();

            var query = from b in ctx.BellSources
                        join s in ctx.StaplesSources on b.OrderNumber equals s.OrderNumber
                        where s.SubLob != "Wireless" && b.SubLob != "Wireless"
                        && s.SubLob == b.SubLob &&
                         (string.IsNullOrEmpty(filterItemDto.RebateValue) || s.RebateType == filterItemDto.RebateValue) &&
                         (string.IsNullOrEmpty(filterItemDto.RebateValue) || b.RebateType == filterItemDto.RebateValue) &&
                         (string.IsNullOrEmpty(filterItemDto.Lob) || s.Lob == filterItemDto.Lob) &&
                         (string.IsNullOrEmpty(filterItemDto.Lob) || b.Lob == filterItemDto.Lob) &&
                         (string.IsNullOrEmpty(filterItemDto.SubLob) || s.SubLob == filterItemDto.SubLob) &&
                         (string.IsNullOrEmpty(filterItemDto.SubLob) || b.SubLob == filterItemDto.SubLob) &&
                         (string.IsNullOrEmpty(filterItemDto.Location) || s.Location == filterItemDto.Location) &&
                         (string.IsNullOrEmpty(filterItemDto.Brand) || s.Brand == filterItemDto.Brand) &&
                         (!filterItemDto.TransactionDateFrom.HasValue || s.TransactionDate >= filterItemDto.TransactionDateFrom) &&
                         (!filterItemDto.TransactionDateFrom.HasValue || b.TransactionDate >= filterItemDto.TransactionDateFrom) &&
                         (!filterItemDto.TransactionDateTo.HasValue || s.TransactionDate <= filterItemDto.TransactionDateTo) &&
                         (!filterItemDto.TransactionDateTo.HasValue || b.TransactionDate <= filterItemDto.TransactionDateTo)
                        select new CompareBellStapleNonCellPhone
                        {
                            BOrderNumber = b.OrderNumber.ToString(),
                            BAmount = b.Amount,
                            BComment = b.Comment.ToString(),
                            BTransactionDate = b.TransactionDate,
                            BCustomerName = b.CustomerName.ToString(),
                            BRebateType = b.RebateType.ToString(),
                            BReconciled = b.Reconciled,
                            BLob = b.Lob,
                            BSublob = b.SubLob,
                            BId = b.Id,

                            SOrderNumber = s.OrderNumber.ToString(),
                            SAmount = s.Amount,
                            SComment = s.Comment.ToString(),
                            STransactionDate = s.TransactionDate,
                            SCustomerName = s.CustomerName.ToString(),
                            SRebateType = s.RebateType.ToString(),
                            SReconciled = s.Reconciled,
                            SLob = s.Lob,
                            SSublob = s.SubLob,
                            SBrand = s.Brand,
                            SLocation = s.Location,
                            SId = s.Id,

                            MatchStatus = (s.Reconciled == true && b.Reconciled == true) ? MatchStatus.Reconciled :
                            ((s.Amount == b.Amount && s.OrderNumber == b.OrderNumber &&
                            s.TransactionDate == b.TransactionDate && s.CustomerName == b.CustomerName && s.Imei == b.Imei && s.Phone == s.Phone) ? MatchStatus.Match : MatchStatus.Missmatch)
                        };

            var bellStaplesCompres = query.ToList();
            return bellStaplesCompres;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    #endregion QUERY FOR UI DATAGRIDS

    #region Update LocalDB while Reconciling

    public async Task<bool> UpdateBellSource(BellSourceDto bellSourceDto)
    {
        try
        {
            using var ctx = await _dbContextFactory.CreateDbContextAsync();

            bellSourceDto.Reconciled = true;
            bellSourceDto.ReconciledBy = _stateContainer.User ?? "USER"; //TODO: it should be replaced with REAL USER
            bellSourceDto.ReconciledDate = DateTime.UtcNow;

            ctx.Update(bellSourceDto);
            ctx.SaveChanges();

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<bool> UpdateBellSource(long Id, string Comment)
    {
        try
        {
            using var ctx = await _dbContextFactory.CreateDbContextAsync();

            var bell = ctx.BellSources.Single(c => c.Id == Id);
            bell.Comment = Comment;
            bell.Reconciled = true;
            bell.ReconciledBy = _stateContainer.User ?? "USER";
            bell.ReconciledDate = DateTime.UtcNow;
            ctx.Update(bell);
            ctx.SaveChanges();

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<bool> UpdateStapleSource(StaplesSourceDto staplesSourceDto)
    {
        try
        {
            using var ctx = await _dbContextFactory.CreateDbContextAsync();

            staplesSourceDto.Reconciled = true;
            staplesSourceDto.ReconciledBy = _stateContainer.User ?? "USER";
            staplesSourceDto.ReconciledDate = DateTime.UtcNow;

            ctx.Update(staplesSourceDto);
            ctx.SaveChanges();

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<bool> UpdateStapleSource(long Id, string Comment)
    {
        try
        {
            using var ctx = await _dbContextFactory.CreateDbContextAsync();

            var staple = ctx.StaplesSources.Single(c => c.Id == Id);
            staple.Comment = Comment;
            staple.Reconciled = true;
            staple.ReconciledBy = _stateContainer.User ?? "USER";
            staple.ReconciledDate = DateTime.UtcNow;
            ctx.Update(staple);
            ctx.SaveChanges();

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    #endregion Update LocalDB while Reconciling

    #region Load LocalDB to UI DataGrid

    public async Task<EntityEntry<BellSourceDto>> GetBellSourceEntry(BellSourceDto record)
    {
        using var ctx = await _dbContextFactory.CreateDbContextAsync();

        var entityEntry = ctx.Entry(record);

        return entityEntry;
    }

    public async Task<EntityEntry<StaplesSourceDto>> GetStapleSourceEntry(StaplesSourceDto record)
    {
        using var ctx = await _dbContextFactory.CreateDbContextAsync();

        var entityEntry = ctx.Entry(record);

        return entityEntry;
    }

    public async Task<bool> LocalDbExist()
    {
        try
        {
            using var ctx = await _dbContextFactory.CreateDbContextAsync();
            if (ctx.BellSources.Count() > 1 && ctx.StaplesSources.Count() > 1)
            {
                return true; ;
            }
            return false;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<bool> PurgeTables()
    {
        try
        {
            using var ctx = await _dbContextFactory.CreateDbContextAsync();
            await ctx.BellSources.ExecuteDeleteAsync();
            await ctx.StaplesSources.ExecuteDeleteAsync();
            await ctx.SyncLogs.ExecuteDeleteAsync();
            await ctx.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            throw;
        }
    }

    #endregion Load LocalDB to UI DataGrid
}