using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_Shop.Models.LoanModels
{
    // ════════════════════════════════════════════════════════════════════════════
    //  CustomerLedger  — one row per customer, live running balance
    // ════════════════════════════════════════════════════════════════════════════
    public class CustomerLedger
    {
        public int Id { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }

        /// <summary>
        /// +ve → customer owes us (Debit / Loan)
        /// -ve → we owe customer (Credit / Advance)
        /// </summary>
     
        public decimal RunningBalance { get; set; } = 0m;

        public DateTime LastTransactionDate { get; set; } = DateTime.Now;

        [MaxLength(500)]
        public string Notes { get; set; }

        // ── Computed (not mapped) ────────────────────────────────────────────
        [NotMapped] public bool IsDebit => RunningBalance > 0;
        [NotMapped] public bool IsCredit => RunningBalance < 0;

        [NotMapped]
        public string BalanceDisplay
        {
            get
            {
                if (RunningBalance == 0) return "Nil";
                return RunningBalance > 0
                    ? $"Dr  Rs. {RunningBalance:N0}"
                    : $"Cr  Rs. {Math.Abs(RunningBalance):N0}";
            }
        }
    }
}
