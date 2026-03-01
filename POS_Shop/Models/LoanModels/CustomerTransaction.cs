using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_Shop.Models.LoanModels
{
    // ════════════════════════════════════════════════════════════════════════════
    //  CustomerTransaction  — immutable audit trail, every debit/credit
    // ════════════════════════════════════════════════════════════════════════════
    public class CustomerTransaction
    {
        public int Id { get; set; }

        [Required] public int CustomerId { get; set; }
        [ForeignKey("CustomerId")] public virtual Customer Customer { get; set; }

        [Required] public DateTime TransactionDate { get; set; } = DateTime.Now;

        [Required][MaxLength(20)] public string TransactionType { get; set; }

        /// <summary>Always stored as a positive value.</summary>
        [Required] public decimal Amount { get; set; }

        /// <summary>D = Debit (balance↑), C = Credit (balance↓)</summary>
        [Required][MaxLength(1)] public string DebitCredit { get; set; }

        /// <summary>Balance snapshot AFTER this transaction.</summary>
         public decimal BalanceAfter { get; set; }

        public int? OrderId { get; set; }
        [ForeignKey("OrderId")] public virtual Order Order { get; set; }

        public int? CustomerPaymentId { get; set; }
        [ForeignKey("CustomerPaymentId")] public virtual CustomerPayment CustomerPayment { get; set; }

        [MaxLength(100)] public string ReferenceNo { get; set; }
        [MaxLength(500)] public string Notes { get; set; }
        [MaxLength(100)] public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; } = false;

        // ── Computed ─────────────────────────────────────────────────────────
        [NotMapped] public bool IsDebit => DebitCredit == "D";
        [NotMapped] public bool IsCredit => DebitCredit == "C";

        [NotMapped]
        public string TypeDisplay
        {
            get
            {
                switch (TransactionType)
                {
                    case TransactionTypes.SaleLoan: return "Sale (Loan)";
                    case TransactionTypes.Payment: return "Payment";
                    case TransactionTypes.Advance: return "Advance";
                    case TransactionTypes.AdvanceUsed: return "Advance Applied";
                    case TransactionTypes.Adjustment: return "Adjustment";
                    case TransactionTypes.Refund: return "Refund";
                    default: return TransactionType;
                }
            }
        }
    }
}
