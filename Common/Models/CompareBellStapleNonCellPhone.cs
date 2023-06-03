namespace Bell.Reconciliation.Common.Models;
public class CompareBellStapleNonCellPhone
{
    public MatchStatus MatchStatus { get; set; }
    public long SId { get; set; }
    public string SLob { get; set; }
    public string SSublob { get; set; }
    public decimal SAmount { get; set; }
    public string SComment { get; set; }
    public string SOrderNumber { get; set; }
    public DateOnly? STransactionDate { get; set; }
    public string SCustomerName { get; set; }
    public string SRebateType { get; set; }
    public string SLocation { get; set; }
    public string SBrand { get; set; }
    public bool SReconciled { get; set; }

    public int BId { get; set; }
    public decimal BAmount { get; set; }
    public string BLob { get; set; }
    public string BSublob { get; set; }
    public string BComment { get; set; }
    public string BOrderNumber { get; set; }
    public DateOnly? BTransactionDate { get; set; }
    public string BCustomerName { get; set; }
    public string BRebateType { get; set; }
    public bool BReconciled { get; set; }
}
