namespace Bell.Reconciliation.Common.Models;

public class BellSourceDto
{
    public int Id { get; set; }
    public long? Amount { get; set; }
    public string? Comment { get; set; }
    public string? CommissionDetails { get; set; }
    public string? CustomerName { get; set; }
    public long? Imei { get; set; }
    public long? OrderNumber { get; set; }
    public long? Phone { get; set; }
    public string? TransactionDate { get; set; }
    public string? Lob { get; set; }

    public string? SubLob { get; set; }
    public string? RebateType { get; set; }
    public bool Reconciled { get; }
    public string? ReconciledBy { get; set; }
    public DateTime ReconciledDate { get; }
    public MatchStatus MatchStatus { get; set; }
}