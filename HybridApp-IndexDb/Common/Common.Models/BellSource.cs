using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bell.Reconciliation.Common.Models
{
    public class BellSource
    {
        public int Id { get; set; }
        public string Phone { get; set; }
        public string Amount { get; set; }
        public string Comment { get; set; }
        public string OrderNumber { get; set; }
        public string IMEI { get; set; }
        public string TransactionDate { get; set; }
        public string CustomerName { get; set; }
        public string CommissionDetails { get; set; }
        public string LOB { get; set; }
    }
}