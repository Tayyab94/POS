using System.ComponentModel.DataAnnotations.Schema;

namespace POS_Shop.Models
{
    public class PurchaseDetail
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }

        // Foreign keys
        public int PurchaseId { get; set; }
        public int ProductId { get; set; }

        // Navigation properties
        [ForeignKey("PurchaseId")]
        public virtual Purchase Purchase { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
    }
}
