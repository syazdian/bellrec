﻿using System;
using System.Collections.Generic;

namespace Bell.Reconciliation.Web.Server.Sqlserver;

public partial class ReqResLog
{
    public long Id { get; set; }

    public string ActivityId { get; set; } = null!;

    public string Direction { get; set; } = null!;

    public DateTime Timestamp { get; set; }

    public string Type { get; set; } = null!;

    public string? Method { get; set; }

    public string? Url { get; set; }

    public string? Headers { get; set; }

    public string? Body { get; set; }

    public string? Exception { get; set; }

    public int? StatusCode { get; set; }

    public string? AdditionalInfo { get; set; }

    public string? RequestId { get; set; }
}