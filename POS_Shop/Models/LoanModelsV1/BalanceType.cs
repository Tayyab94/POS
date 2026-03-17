namespace POS_Shop.Models.LoanModelsV1
{
    public enum BalanceType
    {
        Loan,       // Customer owes us   (Balance > 0)
        Advance,    // We owe customer    (Balance < 0)
        Clear       // Settled            (Balance == 0)
    }


}
