using Bell.Reconciliation.Common.Models;
using Bell.Reconciliation.Common.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Bell.Reconciliation.Frontend.Web.Services.Database;

public class LocalDbRepository : ILocalDbRepository
{
    private readonly ISqliteWasmDbContextFactory<StapleSourceContext> _dbContextFactory;

    public LocalDbRepository(ISqliteWasmDbContextFactory<StapleSourceContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

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

    public async Task<List<BellSourceDto>> GetBellSourceCellPhoneFromLocalDb(FilterItemDto filterItemDto)
    {
        try
        {
            using var ctx = await _dbContextFactory.CreateDbContextAsync();

            var query = ctx.BellSources.Where(b => b.SubLob == "Wireless" && !ctx.StaplesSources.Any(s => s.Phone == b.Phone || s.Imei == b.Imei));
            query = query.Where(c =>
                (string.IsNullOrEmpty(filterItemDto.RebateValue) || c.RebateType == filterItemDto.RebateValue) &&
                (string.IsNullOrEmpty(filterItemDto.Lob) || c.Lob == filterItemDto.Lob) &&
                (string.IsNullOrEmpty(filterItemDto.SubLob) || c.Lob == filterItemDto.SubLob)
            );

            List<BellSourceDto> bellSources = await query.ToListAsync();
            return bellSources;
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
        var query = ctx.StaplesSources.Where(s => s.SubLob == "Wireless" && !ctx.BellSources.Any(b => b.Phone == s.Phone || b.Imei == s.Imei));
        query = query.Where(c =>
         (string.IsNullOrEmpty(filterItemDto.RebateValue) || c.RebateType == filterItemDto.RebateValue) &&
         (string.IsNullOrEmpty(filterItemDto.Lob) || c.Lob == filterItemDto.Lob) &&
         (string.IsNullOrEmpty(filterItemDto.SubLob) || c.SubLob == filterItemDto.SubLob) &&
         (string.IsNullOrEmpty(filterItemDto.Location) || c.Location == filterItemDto.Location) &&
         (string.IsNullOrEmpty(filterItemDto.Brand) || c.Brand == filterItemDto.Brand)
       );

        List<StaplesSourceDto> staplesSources = await query.ToListAsync();
        return staplesSources;
    }

    public async Task<List<CompareBellStapleCellPhone>> GetBellStapleCompareCellPhoneFromLocalDb(FilterItemDto filterItemDto)
    {
        try
        {

            using var ctx = await _dbContextFactory.CreateDbContextAsync();

            var queryJoinByPhone = from b in ctx.BellSources
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
                                  (string.IsNullOrEmpty(filterItemDto.RebateValue) || b.RebateType == filterItemDto.RebateValue)
                                   select new CompareBellStapleCellPhone
                                   {
                                       BPhone = b.Phone.ToString(),
                                       BLob = b.Lob,
                                       BSublob = b.SubLob,
                                       BIMEI = b.Imei.ToString(),
                                       BOrderNumber = b.OrderNumber.ToString(),
                                       BAmount = b.Amount,
                                       BComment = b.Comment.ToString(),
                                       BTransactionDate = b.TransactionDate.ToString(),
                                       BCustomerName = b.CustomerName.ToString(),
                                       BRebateType = b.RebateType.ToString(),
                                       BReconciled = b.Reconciled,

                                       SPhone = s.Phone.ToString(),
                                       SLob = s.Lob,
                                       SSublob = s.SubLob,
                                       SIMEI = s.Imei.ToString(),
                                       SOrderNumber = s.OrderNumber.ToString(),
                                       SAmount = s.Amount,
                                       SComment = s.Comment.ToString(),
                                       STransactionDate = s.TransactionDate.ToString(),
                                       SCustomerName = s.CustomerName.ToString(),
                                       SRebateType = b.RebateType.ToString(),
                                       SReconciled = b.Reconciled,
                                       MatchStatus = (s.Reconciled == true && b.Reconciled == true) ? MatchStatus.Reconciled :
                                       ((s.Amount == b.Amount && s.OrderNumber == b.OrderNumber &&
                                       s.TransactionDate == b.TransactionDate && s.CustomerName == b.CustomerName && s.Imei == b.Imei && s.Phone == s.Phone) ? MatchStatus.Match : MatchStatus.Missmatch)
                                   };
           //);
            var listJoinByPhone = queryJoinByPhone.ToList();

            var queryJoinByImei = from b in ctx.BellSources
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
                                  (string.IsNullOrEmpty(filterItemDto.RebateValue) || b.RebateType == filterItemDto.RebateValue)
                                  select new CompareBellStapleCellPhone
                                  {
                                      BPhone = b.Phone.ToString(),
                                      BLob = b.Lob,
                                      BSublob = b.SubLob,
                                      BIMEI = b.Imei.ToString(),
                                      BOrderNumber = b.OrderNumber.ToString(),
                                      BAmount = b.Amount,
                                      BComment = b.Comment.ToString(),
                                      BTransactionDate = b.TransactionDate.ToString(),
                                      BCustomerName = b.CustomerName.ToString(),
                                      BRebateType = b.RebateType.ToString(),
                                      BReconciled = b.Reconciled,

                                      SPhone = s.Phone.ToString(),
                                      SLob = s.Lob,
                                      SSublob = s.SubLob,
                                      SIMEI = s.Imei.ToString(),
                                      SOrderNumber = s.OrderNumber.ToString(),
                                      SAmount = s.Amount,
                                      SComment = s.Comment.ToString(),
                                      STransactionDate = s.TransactionDate.ToString(),
                                      SCustomerName = s.CustomerName.ToString(),
                                      SRebateType = b.RebateType.ToString(),
                                      SReconciled = b.Reconciled,
                                      SBrand = s.Brand,
                                      SLocation = s.Location,

                                      MatchStatus = (s.Reconciled == true && b.Reconciled == true) ? MatchStatus.Reconciled :
                                      ((s.Amount == b.Amount && s.OrderNumber == b.OrderNumber &&
                                      s.TransactionDate == b.TransactionDate && s.CustomerName == b.CustomerName && s.Imei == b.Imei && s.Phone == s.Phone) ? MatchStatus.Match : MatchStatus.Missmatch)
                                  };


            var listJoinByImei = queryJoinByImei.ToList();

            var bellStaplesCompres = listJoinByPhone.Concat(listJoinByImei).ToList();

            return bellStaplesCompres;
        }
        catch (Exception ex)
        {
            return null;

        }
    }

    public async Task<List<BellSourceDto>> GetBellSourceNonCellPhoneFromLocalDb(FilterItemDto filterItemDto)
    {
        using var ctx = await _dbContextFactory.CreateDbContextAsync();
        var query = ctx.BellSources.Where(b => b.SubLob != "Wireless" && !ctx.StaplesSources.Any(s => s.OrderNumber == b.OrderNumber && s.RebateType == b.RebateType));
        query = query.Where(c =>
          (string.IsNullOrEmpty(filterItemDto.RebateValue) || c.RebateType == filterItemDto.RebateValue) &&
          (string.IsNullOrEmpty(filterItemDto.Lob) || c.Lob == filterItemDto.Lob) &&
          (string.IsNullOrEmpty(filterItemDto.SubLob) || c.SubLob == filterItemDto.SubLob)
      );

        List<BellSourceDto> bellSources = await query.ToListAsync();
        return bellSources;
    }

    public async Task<List<StaplesSourceDto>> GetStapleSourceNonCellPhoneFromLocalDb(FilterItemDto filterItemDto)
    {
        using var ctx = await _dbContextFactory.CreateDbContextAsync();
        var query = ctx.StaplesSources.Where(s => s.SubLob != "Wireless" && !ctx.BellSources.Any(b => s.OrderNumber == b.OrderNumber && s.RebateType == b.RebateType));
        query = query.Where(c =>
          (string.IsNullOrEmpty(filterItemDto.RebateValue) || c.RebateType == filterItemDto.RebateValue) &&
          (string.IsNullOrEmpty(filterItemDto.Lob) || c.Lob == filterItemDto.Lob) &&
          (string.IsNullOrEmpty(filterItemDto.SubLob) || c.SubLob == filterItemDto.SubLob) &&
          (string.IsNullOrEmpty(filterItemDto.Location) || c.Location == filterItemDto.Location) &&
          (string.IsNullOrEmpty(filterItemDto.Brand) || c.Brand == filterItemDto.Brand)
        );

        List<StaplesSourceDto> staplesSources = await query.ToListAsync();
        return staplesSources;
    }

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
                         (string.IsNullOrEmpty(filterItemDto.Brand) || s.Brand == filterItemDto.Brand)
                        select new CompareBellStapleNonCellPhone
                        {
                            BOrderNumber = b.OrderNumber.ToString(),
                            BAmount = b.Amount,
                            BComment = b.Comment.ToString(),
                            BTransactionDate = b.TransactionDate.ToString(),
                            BCustomerName = b.CustomerName.ToString(),
                            BRebateType = b.RebateType.ToString(),
                            BReconciled = b.Reconciled,
                            BLob = b.Lob,
                            BSublob = b.SubLob,

                            SOrderNumber = s.OrderNumber.ToString(),
                            SAmount = s.Amount,
                            SComment = s.Comment.ToString(),
                            STransactionDate = s.TransactionDate.ToString(),
                            SCustomerName = s.CustomerName.ToString(),
                            SRebateType = s.RebateType.ToString(),
                            SReconciled = s.Reconciled,
                            SLob = s.Lob,
                            SSublob = s.SubLob,
                            SBrand = s.Brand,
                            SLocation = s.Location,

                            MatchStatus = (s.Reconciled == true && b.Reconciled == true) ? MatchStatus.Reconciled :
                            ((s.Amount == b.Amount && s.OrderNumber == b.OrderNumber &&
                            s.TransactionDate == b.TransactionDate && s.CustomerName == b.CustomerName && s.Imei == b.Imei && s.Phone == s.Phone) ? MatchStatus.Match : MatchStatus.Missmatch)
                        };

            var bellStaplesCompres = query.ToList();
            return bellStaplesCompres;
        }
        catch(Exception ex)
        {
            return null;

        }
    }

    public async Task<bool> UpdateBellSource(BellSourceDto bellSourceDto)
    {
        try
        {
            using var ctx = await _dbContextFactory.CreateDbContextAsync();

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
            ctx.Update(staple);
            ctx.SaveChanges();

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

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
            await ctx.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            throw;
        }
    }
}