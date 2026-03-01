namespace POS_Shop.Models.LoanModels
{
    // ════════════════════════════════════════════════════════════════════════════
    //  Constants — eliminates magic strings throughout the codebase
    // ════════════════════════════════════════════════════════════════════════════
    public static class TransactionTypes
    {
        public const string SaleLoan = "SaleLoan";
        public const string Payment = "Payment";
        public const string Advance = "Advance";
        public const string AdvanceUsed = "AdvanceUsed";
        public const string Adjustment = "Adjustment";
        public const string Refund = "Refund";
    }
}
