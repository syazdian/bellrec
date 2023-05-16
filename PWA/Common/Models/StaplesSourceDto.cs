namespace Bell.Reconciliation.Common.Models;

public class StaplesSourceDto
{
    public long Id { get; set; }

    public long? Phone { get; set; }

    public long? Amount { get; set; }

    public string? Comment { get; set; }

    public long? OrderNumber { get; set; }

    public string? RebateType { get; set; }

    public string? Product { get; set; }

    public string? Rec { get; set; }

    public string? Imei { get; set; }

    public string? TransactionDate { get; set; }

    public string? SalesPerson { get; set; }

    public string? CustomerName { get; set; }

    public long? TaxCode { get; set; }

    public string? Msf { get; set; }

    public string? DeviceCo { get; set; }

    public string? Location { get; set; }

    public string? Brand { get; set; }

    public string? SubLob { get; set; }
    public bool Reconciled { get; }
    public string? ReconciledBy { get; set; }
    public DateTime ReconciledDate { get; }
    public MatchStatus MatchStatus { get; set; }
}