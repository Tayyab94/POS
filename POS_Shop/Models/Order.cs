using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_Shop.Models
{
    public class Order
    {

        public Order()
        {
            OrderDetails = new List<OrderDetail>();
        }

        public int Id { get; set; }

        public float TotalBill { get; set; }

        public float ReceiveAmount { get; set; }

        public DateTime CreatedDate { get; set; }

        public string InvoiceNumber { get; set; }

        [StringLength(maximumLength: 10)]
        public string paymentType { get; set; } = "Cash";
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        public int? customerId { get; set; } = null;
        [ForeignKey("customerId")]
        public virtual Customer Customer { get; set; }
    }
}
