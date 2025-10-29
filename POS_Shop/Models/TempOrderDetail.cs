using System.ComponentModel.DataAnnotations.Schema;

namespace POS_Shop.Models
{
    public class TempOrderDetail
    {

        public int Id { get; set; }

        public int? ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public string QuantityType { get; set; }

        public float Price { get; set; }

        public string TempInvoiceNumber { get; set; }
        [ForeignKey("TempInvoiceNumber")]
        public virtual TempOrder TempOrder { get; set; }
        public string ProductDetail { get; set; } = string.Empty;

    }
}
