using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_Shop.Models
{
    public class OrderDetail
    {

        public int Id { get; set; }

        public int? ProductId { get; set; }
        public string OtherProductName { get; set; }
        public int Quantity { get; set; }
        public string QuantityType { get; set; }

        public float Price { get; set; }

        public DateTime CreatedDate { get; set; }
        public int OrderId { get; set; }
        [ForeignKey("OrderId")]
        public virtual Order Order { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
    }
}
