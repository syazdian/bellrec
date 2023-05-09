using System;
using System.Collections.Generic;

namespace Bell.Reconciliation.Web.Server.Data;

public partial class StaplesSource
{
    public long Id { get; set; }

    public long? Phone { get; set; }

    public string? Amount { get; set; }

    public string? Comment { get; set; }

    public string? OrderNumber { get; set; }

    public string? RebateType { get; set; }

    public string? Product { get; set; }

    public string? Rec { get; set; }

    public string? Imei { get; set; }

    public string? TransactionDate { get; set; }

    public string? SalesPerson { get; set; }

    public string? CustomerName { get; set; }

    public string? TaxCode { get; set; }

    public string? Msf { get; set; }

    public string? DeviceCo { get; set; }

    public string? Location { get; set; }

    public string? Brand { get; set; }
}
