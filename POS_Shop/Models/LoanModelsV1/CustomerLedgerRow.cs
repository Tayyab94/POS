using System;

namespace POS_Shop.Models.LoanModelsV1
{
    // ─── DTOs for UI ─────────────────────────────────────────────────────────

    public class CustomerLedgerRow
    {
        public int Id { get; set; }
        public DateTime EntryDate { get; set; }
        public string EntryTypeDisplay { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public decimal Balance { get; set; }
        public string Note { get; set; }
        public string ReferenceType { get; set; }
        public int? ReferenceId { get; set; }
        public string CreatedBy { get; set; }

        // UI helpers
        public string DebitDisplay => Debit > 0 ? Debit.ToString("N2") : "-";
        public string CreditDisplay => Credit > 0 ? Credit.ToString("N2") : "-";
        public string BalanceDisplay => Math.Abs(Balance).ToString("N2");
        public string BalanceTypeDisplay => Balance > 0 ? "Loan" : Balance < 0 ? "Advance" : "Clear";
    }


}
