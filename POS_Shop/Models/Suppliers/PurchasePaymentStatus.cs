//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace POS_Shop.Models.Suppliers
//{
//    // ══════════════════════════════════════════════════════════════════════════
//    //  ENUMS
//    // ══════════════════════════════════════════════════════════════════════════

//    /// <summary>
//    /// Payment status of a single Purchase invoice.
//    /// Automatically updated every time a payment is recorded or deleted.
//    /// </summary>
//    public enum PurchasePaymentStatus
//    {
//        Pending = 0,   // No payment at all
//        PartiallyPaid = 1,   // Some payment made, balance still remaining
//        Paid = 2    // Fully settled
//    }

//    /// <summary>How the supplier was paid.</summary>
//    public enum PaymentMethod
//    {
//        Cash = 0,
//        BankTransfer = 1,
//        Cheque = 2,
//        OnlineTransfer = 3
//    }

//    // ══════════════════════════════════════════════════════════════════════════
//    //  PURCHASE  — one delivery / invoice from supplier
//    // ══════════════════════════════════════════════════════════════════════════

//    [Table("Purchases")]
//    public class Purchase
//    {
//        [Key]
//        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
//        public int Id { get; set; }

//        /// <summary>System-generated e.g. INV-00001</summary>
//        [Required]
//        [StringLength(30)]
//        public string InvoiceNumber { get; set; }

//        /// <summary>Supplier's own bill number (optional, for matching physical bills)</summary>
//        [StringLength(50)]
//        public string SupplierReferenceNo { get; set; }

//        [Required]
//        public DateTime PurchaseDate { get; set; } = DateTime.Now;

//        [Required]
//        public int SupplierId { get; set; }

//        [ForeignKey("SupplierId")]
//        public virtual Supplier Supplier { get; set; }

//        // ── Financials ────────────────────────────────────────────────────────

//        /// <summary>Sum of all PurchaseItems.TotalPrice (before discount)</summary>
//        public decimal TotalAmount { get; set; }

//        /// <summary>Discount given on this invoice</summary>
//        public decimal Discount { get; set; }

//        /// <summary>TotalAmount − Discount = what we owe for this invoice</summary>
//        public decimal NetAmount { get; set; }

//        /// <summary>
//        /// Running total of payments allocated to this invoice via SupplierPaymentDetails.
//        /// DO NOT set manually — always use Purchase.RecalculateFromPayments().
//        /// </summary>
//        public decimal TotalPaid { get; set; }

//        /// <summary>NetAmount − TotalPaid. Always >= 0.</summary>
//        public decimal Balance { get; set; }

//        // ── Status ────────────────────────────────────────────────────────────

//        /// <summary>
//        /// Pending → PartiallyPaid → Paid.
//        /// Auto-updated by RecalculateFromPayments().
//        /// </summary>
//        public PurchasePaymentStatus PaymentStatus { get; set; } = PurchasePaymentStatus.Pending;

//        // ── Audit ─────────────────────────────────────────────────────────────
//        public string Notes { get; set; }
//        public bool IsDeleted { get; set; } = false;
//        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
//        public DateTime? UpdatedAt { get; set; }

//        // ── Navigation ────────────────────────────────────────────────────────
//        public virtual ICollection<PurchaseItem> PurchaseItems { get; set; }
//            = new List<PurchaseItem>();

//        public virtual ICollection<SupplierPaymentDetail> PaymentDetails { get; set; }
//            = new List<SupplierPaymentDetail>();

//        // ── Business Logic ────────────────────────────────────────────────────

//        /// <summary>
//        /// Call this after adding/deleting any SupplierPaymentDetail that touches
//        /// this purchase. Recalculates TotalPaid, Balance and PaymentStatus.
//        /// </summary>
//        public void RecalculateFromPayments()
//        {
//            decimal paid = 0;
//            foreach (var detail in PaymentDetails)
//                paid += detail.AmountAllocated;

//            TotalPaid = paid;
//            Balance = Math.Max(0, NetAmount - TotalPaid);

//            if (TotalPaid <= 0)
//                PaymentStatus = PurchasePaymentStatus.Pending;
//            else if (Balance > 0)
//                PaymentStatus = PurchasePaymentStatus.PartiallyPaid;
//            else
//                PaymentStatus = PurchasePaymentStatus.Paid;

//            UpdatedAt = DateTime.UtcNow;
//        }
//    }

//    // ══════════════════════════════════════════════════════════════════════════
//    //  PURCHASE ITEM  — one line inside a purchase invoice
//    // ══════════════════════════════════════════════════════════════════════════

//    [Table("PurchaseItems")]
//    public class PurchaseItem
//    {
//        [Key]
//        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
//        public int Id { get; set; }

//        [Required]
//        public int PurchaseId { get; set; }

//        [ForeignKey("PurchaseId")]
//        public virtual Purchase Purchase { get; set; }

//        [Required]
//        public int ProductId { get; set; }

//        [ForeignKey("ProductId")]
//        public virtual Product Product { get; set; }

//        public int? ProductUnitId { get; set; }

//        [ForeignKey("ProductUnitId")]
//        public virtual ProductUnit ProductUnit { get; set; }

//        public decimal Quantity { get; set; }
//        public decimal PurchasePrice { get; set; }
//        public decimal TotalPrice { get; set; }
//    }

//    // ══════════════════════════════════════════════════════════════════════════
//    //  SUPPLIER PAYMENT  — one payment run (weekly, monthly, ad-hoc)
//    //
//    //  A single payment can settle ONE or MANY purchase invoices.
//    //
//    //  Example:
//    //    User pays supplier Rs 50,000 on Friday.
//    //    → INV-001 balance was 20,000  → allocate 20,000  (now Paid)
//    //    → INV-002 balance was 40,000  → allocate 30,000  (now PartiallyPaid)
//    //    Total allocated = 50,000 = TotalAmountPaid ✔
//    // ══════════════════════════════════════════════════════════════════════════

//    [Table("SupplierPayments")]
//    public class SupplierPayment
//    {
//        [Key]
//        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
//        public int Id { get; set; }

//        /// <summary>System-generated e.g. PAY-00001</summary>
//        [Required]
//        [StringLength(30)]
//        public string PaymentNumber { get; set; }

//        [Required]
//        public int SupplierId { get; set; }

//        [ForeignKey("SupplierId")]
//        public virtual Supplier Supplier { get; set; }

//        [Required]
//        public DateTime PaymentDate { get; set; } = DateTime.Now;

//        /// <summary>Total cash/transfer amount given to supplier in this run</summary>
//        public decimal TotalAmountPaid { get; set; }

//        /// <summary>
//        /// Sum of all PaymentDetails.AmountAllocated.
//        /// Must equal TotalAmountPaid (validated before save).
//        /// </summary>
//        public decimal TotalAllocated { get; set; }

//        public PaymentMethod PaymentMethod { get; set; } = PaymentMethod.Cash;

//        /// <summary>Cheque no / bank transaction ID / UPI reference etc.</summary>
//        [StringLength(100)]
//        public string TransactionReference { get; set; }

//        public string Notes { get; set; }
//        public bool IsDeleted { get; set; } = false;
//        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

//        public virtual ICollection<SupplierPaymentDetail> PaymentDetails { get; set; }
//            = new List<SupplierPaymentDetail>();
//    }

//    // ══════════════════════════════════════════════════════════════════════════
//    //  SUPPLIER PAYMENT DETAIL — allocates part of a payment to one invoice
//    // ══════════════════════════════════════════════════════════════════════════

//    [Table("SupplierPaymentDetails")]
//    public class SupplierPaymentDetail
//    {
//        [Key]
//        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
//        public int Id { get; set; }

//        [Required]
//        public int SupplierPaymentId { get; set; }

//        [ForeignKey("SupplierPaymentId")]
//        public virtual SupplierPayment SupplierPayment { get; set; }

//        [Required]
//        public int PurchaseId { get; set; }

//        [ForeignKey("PurchaseId")]
//        public virtual Purchase Purchase { get; set; }

//        /// <summary>
//        /// How much of this payment is applied to the linked invoice.
//        /// Cannot exceed Purchase.Balance at time of recording.
//        /// </summary>
//        public decimal AmountAllocated { get; set; }

//        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
//    }
//}



using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_Shop.Models.Suppliers
{
    // ══════════════════════════════════════════════════════════════════════════
    //  ENUMS
    // ══════════════════════════════════════════════════════════════════════════

    public enum PurchasePaymentStatus
    {
        Pending = 0,   // No payment at all
        PartiallyPaid = 1,   // Some payment, balance still remaining
        Paid = 2    // Fully settled
    }

    public enum PaymentMethod
    {
        Cash = 0,
        BankTransfer = 1,
        Cheque = 2,
        OnlineTransfer = 3
    }

    // ══════════════════════════════════════════════════════════════════════════
    //  PURCHASE  — one delivery / invoice from a supplier
    // ══════════════════════════════════════════════════════════════════════════

    [Table("Purchases")]
    public class Purchase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, StringLength(30)]
        public string InvoiceNumber { get; set; }

        [StringLength(50)]
        public string SupplierReferenceNo { get; set; }

        [Required]
        public DateTime PurchaseDate { get; set; } = DateTime.Now;

        [Required]
        public int SupplierId { get; set; }

        [ForeignKey("SupplierId")]
        public virtual Supplier Supplier { get; set; }

        // ── Financials ────────────────────────────────────────────────────────

        /// <summary>Sum of all PurchaseItems.TotalPrice before discount.</summary>
        public decimal TotalAmount { get; set; }

        public decimal Discount { get; set; }

        /// <summary>TotalAmount − Discount.</summary>
        public decimal NetAmount { get; set; }

        /// <summary>
        /// Total cash allocated to this invoice across all payment runs.
        /// Never set manually — always updated by RecalculateFromPayments().
        /// </summary>
        public decimal TotalPaid { get; set; }

        /// <summary>NetAmount − TotalPaid. Always >= 0.</summary>
        public decimal Balance { get; set; }

        // ── Payment Status ────────────────────────────────────────────────────

        /// <summary>
        /// Automatically transitions: Pending → PartiallyPaid → Paid.
        /// Updated by RecalculateFromPayments() — never set manually.
        /// </summary>
        public PurchasePaymentStatus PaymentStatus { get; set; } = PurchasePaymentStatus.Pending;

        // ── Audit ─────────────────────────────────────────────────────────────

        public string Notes { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        // ── Navigation ────────────────────────────────────────────────────────

        public virtual ICollection<PurchaseItem> PurchaseItems { get; set; }
            = new List<PurchaseItem>();

        public virtual ICollection<SupplierPaymentDetail> PaymentDetails { get; set; }
            = new List<SupplierPaymentDetail>();

        // ── Business Logic ────────────────────────────────────────────────────

        /// <summary>
        /// Call after adding or removing a SupplierPaymentDetail on this invoice.
        /// Recalculates TotalPaid, Balance, PaymentStatus, and stamps UpdatedAt.
        /// </summary>
        public void RecalculateFromPayments()
        {
            decimal paid = 0;
            foreach (var d in PaymentDetails)
                paid += d.AmountAllocated;

            TotalPaid = paid;
            Balance = Math.Max(0, NetAmount - TotalPaid);

            if (TotalPaid <= 0)
                PaymentStatus = PurchasePaymentStatus.Pending;
            else if (Balance > 0)
                PaymentStatus = PurchasePaymentStatus.PartiallyPaid;
            else
                PaymentStatus = PurchasePaymentStatus.Paid;

            UpdatedAt = DateTime.UtcNow;
        }
    }

    // ══════════════════════════════════════════════════════════════════════════
    //  PURCHASE ITEM  — one line inside a purchase invoice
    //
    //  CHANGE vs original:
    //    + IsDeleted added (was missing — PurchaseForm soft-delete code
    //      referenced i.IsDeleted which would have been a compile error).
    // ══════════════════════════════════════════════════════════════════════════

    [Table("PurchaseItems")]
    public class PurchaseItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int PurchaseId { get; set; }

        [ForeignKey("PurchaseId")]
        public virtual Purchase Purchase { get; set; }

        [Required]
        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

        public int? ProductUnitId { get; set; }

        [ForeignKey("ProductUnitId")]
        public virtual ProductUnit ProductUnit { get; set; }

        public decimal Quantity { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal TotalPrice { get; set; }

        /// <summary>
        /// Soft-delete.  Set to true when user removes a line item while
        /// editing an existing purchase.  Row is hidden from the grid but
        /// kept in the DB so history is never lost.
        /// </summary>
        public bool IsDeleted { get; set; } = false;
    }

    // ══════════════════════════════════════════════════════════════════════════
    //  SUPPLIER PAYMENT  — one payment run covering one or many invoices
    // ══════════════════════════════════════════════════════════════════════════

    [Table("SupplierPayments")]
    public class SupplierPayment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, StringLength(30)]
        public string PaymentNumber { get; set; }

        [Required]
        public int SupplierId { get; set; }

        [ForeignKey("SupplierId")]
        public virtual Supplier Supplier { get; set; }

        [Required]
        public DateTime PaymentDate { get; set; } = DateTime.Now;

        /// <summary>Cash / transfer total given to supplier this run.</summary>
        public decimal TotalAmountPaid { get; set; }

        /// <summary>Sum of all detail rows. Must equal TotalAmountPaid before save.</summary>
        public decimal TotalAllocated { get; set; }

        public PaymentMethod PaymentMethod { get; set; } = PaymentMethod.Cash;

        [StringLength(100)]
        public string TransactionReference { get; set; }

        public string Notes { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public virtual ICollection<SupplierPaymentDetail> PaymentDetails { get; set; }
            = new List<SupplierPaymentDetail>();
    }

    // ══════════════════════════════════════════════════════════════════════════
    //  SUPPLIER PAYMENT DETAIL — allocates part of a payment to one invoice
    // ══════════════════════════════════════════════════════════════════════════

    [Table("SupplierPaymentDetails")]
    public class SupplierPaymentDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int SupplierPaymentId { get; set; }

        [ForeignKey("SupplierPaymentId")]
        public virtual SupplierPayment SupplierPayment { get; set; }

        [Required]
        public int PurchaseId { get; set; }

        [ForeignKey("PurchaseId")]
        public virtual Purchase Purchase { get; set; }

        /// <summary>
        /// Amount from this payment run applied to the linked invoice.
        /// Validated not to exceed Purchase.Balance at time of save.
        /// </summary>
        public decimal AmountAllocated { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
