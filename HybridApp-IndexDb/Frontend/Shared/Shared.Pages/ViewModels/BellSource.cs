using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bell.Reconciliation.Common.Models
{
    public class CompareBellStaple
    {
        public int Id { get; set; }
        public string BPhone { get; set; }
        public string SPhone { get; set; }
        public string BAmount { get; set; }
        public string SAmount { get; set; }
        public string BComment { get; set; }
        public string SComment { get; set; }
        public string BOrderNumber { get; set; }
        public string SOrderNumber { get; set; }
        public string BIMEI { get; set; }
        public string SIMEI { get; set; }
        public string BTransactionDate { get; set; }
        public string STransactionDate { get; set; }
        public string BCustomerName { get; set; }
        public string SCustomerName { get; set; }
        public string BCommissionDetails { get; set; }
        public string SCommissionDetails { get; set; }
        public string BLOB { get; set; }
        public string SLOB { get; set; }
    }
}