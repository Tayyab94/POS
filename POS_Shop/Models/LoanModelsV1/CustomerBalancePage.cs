using System.Collections.Generic;

namespace POS_Shop.Models.LoanModelsV1
{
    /// <summary>One page of customers returned to the dashboard grid.</summary>
    public class CustomerBalancePage
    {
        public List<CustomerBalanceSummary> Rows { get; set; } = new List<CustomerBalanceSummary>();
        public bool HasNextPage { get; set; }
        public bool HasPrevPage { get; set; }
    }


}
