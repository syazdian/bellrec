﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bell.Reconciliation.Common.Models
{
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
    }
}