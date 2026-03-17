namespace POS_Shop.Models.LoanModelsV1
{
    public class LedgerSummary
    {
        public decimal TotalDebit { get; set; }
        public decimal TotalCredit { get; set; }
        public decimal CurrentBalance { get; set; }
        public decimal OpeningBalance { get; set; }
        public BalanceType BalanceType => CurrentBalance > 0 ? BalanceType.Loan : CurrentBalance < 0 ? BalanceType.Advance : BalanceType.Clear;
    }


}
