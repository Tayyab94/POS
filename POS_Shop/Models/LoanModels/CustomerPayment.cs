using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_Shop.Models.LoanModels
{
    // ════════════════════════════════════════════════════════════════════════════
    //  CustomerPayment  — standalone payment record (not tied to one order)
    // ════════════════════════════════════════════════════════════════════════════
    public class CustomerPayment
    {
        public int Id { get; set; }

        [Required] public int CustomerId { get; set; }
        [ForeignKey("CustomerId")] public virtual Customer Customer { get; set; }

        [Required] public DateTime PaymentDate { get; set; } = DateTime.Now;

        [Required] public decimal AmountPaid { get; set; }

        [Required][MaxLength(30)] public string PaymentMethod { get; set; } = PaymentMethods.Cash;

        [MaxLength(100)] public string ReferenceNo { get; set; }
        [MaxLength(100)] public string TransactionId { get; set; }
        [MaxLength(500)] public string Notes { get; set; }
        [MaxLength(100)] public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

         public decimal BalanceBefore { get; set; }
         public decimal BalanceAfter { get; set; }

        public bool IsDeleted { get; set; } = false;
        [MaxLength(100)] public string DeletedBy { get; set; }
        public DateTime? DeletedAt { get; set; }
        [MaxLength(200)] public string DeleteReason { get; set; }
    }
}
