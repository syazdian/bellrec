using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bell.Reconciliation.Common.Models
{
    //TODO: replace with StaplesSource
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

        public long? Imei { get; set; }

        public string? TransactionDate { get; set; }

        public string? SalesPerson { get; set; }

        public string? CustomerName { get; set; }

        public long? TaxCode { get; set; }

        public string? Msf { get; set; }

        public string? DeviceCo { get; set; }

        public string? Location { get; set; }

        public string? Brand { get; set; }
    }
}