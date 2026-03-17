using System;

namespace POS_Shop.Models.LoanModelsV1
{
    public class CustomerBalanceSummary
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string ContactNo { get; set; }
        public string CityName { get; set; }
        public decimal Balance { get; set; }
        public BalanceType BalanceType => Balance > 0 ? BalanceType.Loan : Balance < 0 ? BalanceType.Advance : BalanceType.Clear;
        public string BalanceDisplay => Math.Abs(Balance).ToString("N2");
        public DateTime? LastTransactionDate { get; set; }
    }


}
