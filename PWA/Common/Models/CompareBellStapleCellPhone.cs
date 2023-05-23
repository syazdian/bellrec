namespace Bell.Reconciliation.Common.Models;
    public class CompareBellStapleCellPhone
    {
        public MatchStatus MatchStatus { get; set; }
        public string SPhone { get; set; }
        public Decimal SAmount { get; set; }
        public string SComment { get; set; }
        public string SOrderNumber { get; set; }
        public string SIMEI { get; set; }
        public string STransactionDate { get; set; }
        public string SCustomerName { get; set; }
        public string SCommissionDetails { get; set; }
        public string SRebateType { get; set; }
        public bool SReconciled { get; set; }

        public string BPhone { get; set; }
        public decimal BAmount { get; set; }
        public string BComment { get; set; }
        public string BOrderNumber { get; set; }
        public string BIMEI { get; set; }
        public string BTransactionDate { get; set; }
        public string BCustomerName { get; set; }
        public string BCommissionDetails { get; set; }
        public string BRebateType { get; set; }
        public bool BReconciled { get; set; }
    }
