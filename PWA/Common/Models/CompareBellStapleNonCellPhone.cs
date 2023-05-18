﻿namespace Bell.Reconciliation.Common.Models;
    public class CompareBellStapleNonCellPhone
    {
        public MatchStatus MatchStatus 
        {
        get
        {
            if (SReconciled == true && BReconciled == true)
                return MatchStatus.Reconciled; 
            else
            {
                if (SAmount == BAmount && SOrderNumber == BOrderNumber && STransactionDate == BTransactionDate && SCustomerName == BCustomerName)
                {
                    return MatchStatus.Match;
                }
                else
                {
                    return MatchStatus.Missmatch;
                }

            }
        }
        }

        public decimal SAmount { get; set; }
        public string SComment { get; set; }
        public string SOrderNumber { get; set; }
        public string STransactionDate { get; set; }
        public string SCustomerName { get; set; }
        public string SRebateType { get; set; }
        public bool SReconciled { get; set; }

        public decimal BAmount { get; set; }
        public string BComment { get; set; }
        public string BOrderNumber { get; set; }
        public string BTransactionDate { get; set; }
        public string BCustomerName { get; set; }
        public string BRebateType { get; set; }
        public bool BReconciled { get; set; }
    }
