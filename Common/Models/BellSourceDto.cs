namespace Bell.Reconciliation.Common.Models;

public class BellSourceDto
{
    public int Id { get; set; }
    public Decimal Amount { get; set; }
    public string? Comment { get; set; } = string.Empty;
    public string? CustomerName { get; set; }
    public string? Imei { get; set; } = string.Empty;
    public long? OrderNumber { get; set; }
    public long? Phone { get; set; }
    public DateTime? TransactionDate { get; set; }
    public string? Lob { get; set; }

    public string? SubLob { get; set; }
    public string? RebateType { get; set; }
    public bool Reconciled { get; set; }
    public string? ReconciledBy { get; set; } = string.Empty;
    public DateTime? ReconciledDate { get; set; }
    public MatchStatus MatchStatus { get; set; }
    public DateTime? UpdateDate { get; set; }
}