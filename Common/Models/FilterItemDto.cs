namespace Bell.Reconciliation.Common.Models;

public class FilterItemDto
{
    public string? Lob { get; set; }
    public string? SubLob { get; set; }
    public string? Brand { get; set; }
    public string? Location { get; set; }
    public string? RebateValue { get; set; }
    public DateOnly? TransactionDateFrom { get; set; }
    public DateOnly? TransactionDateTo { get; set; }
}