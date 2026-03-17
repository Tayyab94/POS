using System;

namespace POS_Shop.Models.LoanModelsV1
{
    /// <summary>Internal raw SQL result for paginated grid query.</summary>
    public class CustomerBalanceSummaryRaw
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string ContactNo { get; set; }
        public decimal Balance { get; set; }
        public DateTime? LastTransactionDate { get; set; }
    }


}
