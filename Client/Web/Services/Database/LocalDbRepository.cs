using Bell.Reconciliation.Common.Models;
using Bell.Reconciliation.Common.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System;

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

    public async Task<List<BellSourceDto>> GetBellSourceCellPhoneFromLocalDb()
    {
        using var ctx = await _dbContextFactory.CreateDbContextAsync();

        var query = ctx.BellSources.Where(b => b.SubLob == "Wireless" && !ctx.StaplesSources.Any(s => s.Phone == b.Phone || s.Imei == b.Imei));

        List<BellSourceDto> bellSources = await query.ToListAsync();
        return bellSources;
    }

    public async Task<List<StaplesSourceDto>> GetStapleSourceCellPhoneFromLocalDb()
    {
        using var ctx = await _dbContextFactory.CreateDbContextAsync();
        var query = ctx.StaplesSources.Where(s => s.SubLob == "Wireless" && !ctx.BellSources.Any(b => b.Phone == s.Phone || b.Imei == s.Imei));

        List<StaplesSourceDto> staplesSources = await query.ToListAsync();
        return staplesSources;
    }

    public async Task<List<CompareBellStapleCellPhone>> GetBellStapleCompareCellPhoneFromLocalDb()
    {
        using var ctx = await _dbContextFactory.CreateDbContextAsync();
        //FormattableString query = $"SELECT stp.Amount as SAmount, stp.Phone as SPhone, bll.Amount as BAmount, bll.Phone as BPhone FROM \"BellSource\" as bll\r\njoin \"StaplesSource\" as stp on bll.Id = stp.id where bll.SubLob = 'Wireless' and stp.SubLob = 'Wireless' ";
        //var bellStaplesCompres = ctx.Database.SqlQuery<CompareBellStapleCellPhoneDto>(query).ToList();

        var query_joinByPhone = from b in ctx.BellSources
                     join s in ctx.StaplesSources on b.Phone equals s.Phone
                     where s.SubLob == "Wireless" && b.SubLob == "Wireless"
                     && s.RebateType == b.RebateType
                     select new CompareBellStapleCellPhone
                     {
                         BPhone = b.Phone.ToString(),
                         BIMEI = b.Imei.ToString(),
                         BOrderNumber = b.OrderNumber.ToString(),
                         BAmount = b.Amount,
                         BComment = b.Comment.ToString(),
                         BTransactionDate = b.TransactionDate.ToString(),
                         BCustomerName = b.CustomerName.ToString(),
                         BRebateType = b.RebateType.ToString(),
                         BReconciled = b.Reconciled,

                         SPhone = s.Phone.ToString(),
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
        var list_joinByPhone = query_joinByPhone.ToList();

        var query_joinByImei = from b in ctx.BellSources
                     join s in ctx.StaplesSources on b.Imei equals s.Imei
                     where s.SubLob == "Wireless" && b.SubLob == "Wireless"
                     && s.Phone != b.Phone
                     && s.RebateType == b.RebateType
                     select new CompareBellStapleCellPhone
                     {
                         BPhone = b.Phone.ToString(),
                         BIMEI = b.Imei.ToString(),
                         BOrderNumber = b.OrderNumber.ToString(),
                         BAmount = b.Amount,
                         BComment = b.Comment.ToString(),
                         BTransactionDate = b.TransactionDate.ToString(),
                         BCustomerName = b.CustomerName.ToString(),
                         BRebateType = b.RebateType.ToString(),
                         BReconciled = b.Reconciled,

                         SPhone = s.Phone.ToString(),
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
        var list_joinByImei = query_joinByImei.ToList();


        var bellStaplesCompres = list_joinByPhone.Concat(list_joinByImei).ToList();

        return bellStaplesCompres;
    }

    public async Task<List<BellSourceDto>> GetBellSourceNonCellPhoneFromLocalDb()
    {
        using var ctx = await _dbContextFactory.CreateDbContextAsync();
        var query = ctx.BellSources.Where(b => b.SubLob != "Wireless" && !ctx.StaplesSources.Any(s => s.OrderNumber == b.OrderNumber && s.RebateType == b.RebateType));

        List<BellSourceDto> bellSources = await query.ToListAsync();
        return bellSources;
    }

    public async Task<List<StaplesSourceDto>> GetStapleSourceNonCellPhoneFromLocalDb()
    {
        using var ctx = await _dbContextFactory.CreateDbContextAsync();
        var query = ctx.StaplesSources.Where(s => s.SubLob != "Wireless" && !ctx.BellSources.Any(b => s.OrderNumber == b.OrderNumber && s.RebateType == b.RebateType));

        List<StaplesSourceDto> staplesSources = await query.ToListAsync();
        return staplesSources;
    }

    public async Task<List<CompareBellStapleNonCellPhone>> GetBellStapleCompareNonCellPhoneFromLocalDb()
    {
        using var ctx = await _dbContextFactory.CreateDbContextAsync();
        //FormattableString query = $"SELECT stp.Amount as SAmount, stp.Phone as SPhone, bll.Amount as BAmount, bll.Phone as BPhone FROM \"BellSource\" as bll\r\njoin \"StaplesSource\" as stp on bll.Id = stp.id where bll.SubLob = 'Wireless' and stp.SubLob = 'Wireless' ";
        //var bellStaplesCompres = ctx.Database.SqlQuery<CompareBellStapleCellPhoneDto>(query).ToList();

        var query = from b in ctx.BellSources
                    join s in ctx.StaplesSources on b.OrderNumber equals s.OrderNumber
                    where s.SubLob != "Wireless" && b.SubLob != "Wireless"
                    && s.SubLob == b.SubLob
                    select new CompareBellStapleNonCellPhone
                    {
                        BOrderNumber = b.OrderNumber.ToString(),
                        BAmount = b.Amount,
                        BComment = b.Comment.ToString(),
                        BTransactionDate = b.TransactionDate.ToString(),
                        BCustomerName = b.CustomerName.ToString(),
                        BRebateType = b.RebateType.ToString(),
                        BReconciled = b.Reconciled,

                        SOrderNumber = s.OrderNumber.ToString(),
                        SAmount = s.Amount,
                        SComment = s.Comment.ToString(),
                        STransactionDate = s.TransactionDate.ToString(),
                        SCustomerName = s.CustomerName.ToString(),
                        SRebateType = s.RebateType.ToString(),
                        SReconciled = s.Reconciled,
                        MatchStatus = (s.Reconciled == true && b.Reconciled == true) ? MatchStatus.Reconciled :
                        ((s.Amount == b.Amount && s.OrderNumber == b.OrderNumber &&
                        s.TransactionDate == b.TransactionDate && s.CustomerName == b.CustomerName && s.Imei == b.Imei && s.Phone == s.Phone) ? MatchStatus.Match : MatchStatus.Missmatch)

                    };
        var bellStaplesCompres = query.ToList();

        return bellStaplesCompres;
    }


    public async Task<bool> LocalDbExist()
    {
        try
        {
            using var ctx = await _dbContextFactory.CreateDbContextAsync();
            if (ctx.BellSources.Count() > 1)
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
}