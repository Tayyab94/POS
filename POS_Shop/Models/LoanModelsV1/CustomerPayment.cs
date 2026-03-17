using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_Shop.Models.LoanModelsV1
{
    [Table("CustomerPayments")]
    public class CustomerPayment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Required]
        public DateTime PaymentDate { get; set; } = DateTime.Now;

        [Required]
        
        public decimal Amount { get; set; }

        [Required]
        [MaxLength(30)]
        public string PaymentMethod { get; set; }  // PaymentMethod.ToString()

        [MaxLength(200)]
        public string ReferenceNo { get; set; }   // Cheque no / bank ref

        [MaxLength(500)]
        public string Note { get; set; }

        public int? LedgerEntryId { get; set; }

        [MaxLength(100)]
        public string CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }
    }


}
