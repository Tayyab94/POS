namespace POS_Shop.Models.LoanModelsV1
{
    // ─── Dashboard DTOs ───────────────────────────────────────────────────────

    /// <summary>KPI totals for the dashboard header — always full-business accurate.</summary>
    public class DashboardKpi
    {
        public decimal TotalLoanAmount { get; set; }
        public decimal TotalAdvanceAmount { get; set; }
        public int LoanCustomerCount { get; set; }
        public int AdvanceCustomerCount { get; set; }
        public int ClearCustomerCount { get; set; }
    }


}
