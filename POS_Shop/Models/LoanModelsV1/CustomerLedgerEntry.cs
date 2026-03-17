using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_Shop.Models.LoanModelsV1
{
    // ─── EF Entities ─────────────────────────────────────────────────────────

    [Table("CustomerLedger")]
    public class CustomerLedgerEntry
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Required]
        public DateTime EntryDate { get; set; } = DateTime.Now;

        [Required]
        [MaxLength(30)]
        public string EntryType { get; set; }   // LedgerEntryType.ToString()

        
        public decimal Debit { get; set; } = 0;   // Customer owes  (+)

        
        public decimal Credit { get; set; } = 0;  // Customer paid  (+)

        [Required]
        
        public decimal Balance { get; set; }       // Running balance (Debit - Credit cumulative)

        public int? ReferenceId { get; set; }      // OrderId / PaymentId

        [MaxLength(30)]
        public string ReferenceType { get; set; }  // "ORDER" / "PAYMENT" / "ADVANCE" / "ADJUSTMENT"

        [MaxLength(500)]
        public string Note { get; set; }

        [MaxLength(100)]
        public string CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Navigation
        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }
    }


}
