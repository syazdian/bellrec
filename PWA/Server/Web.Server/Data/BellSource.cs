using System;
using System.Collections.Generic;

namespace Bell.Reconciliation.Web.Server.Data;

public partial class BellSource
{
    public long Id { get; set; }

    public long? Phone { get; set; }

    public long? Amount { get; set; }

    public string? Comment { get; set; }

    public long? OrderNumber { get; set; }

    public long? Imei { get; set; }

    public string? TransactionDate { get; set; }

    public string? CustomerName { get; set; }

    public string? CommissionDetails { get; set; }

    public string? Lob { get; set; }
}
