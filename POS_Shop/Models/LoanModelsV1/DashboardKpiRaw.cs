namespace POS_Shop.Models.LoanModelsV1
{
    /// <summary>Internal raw SQL result for KPI query.</summary>
    public class DashboardKpiRaw
    {
        public decimal? TotalLoan { get; set; }
        public decimal? TotalAdvance { get; set; }
        public int LoanCount { get; set; }
        public int AdvanceCount { get; set; }
        public int ClearCount { get; set; }
    }


}
