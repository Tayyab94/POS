using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_Shop.Models
{
    public class TempOrder
    {

        public TempOrder()
        {
            OrderDetails = new List<TempOrderDetail>();
        }

        public float TotalBill { get; set; }

        
        public DateTime CreatedDate { get; set; }

        [Key]
        public string InvoiceNumber { get; set; }


        public int? customerId { get; set; } = null;

        public string CustomerName { get; set; }

        public virtual ICollection<TempOrderDetail> OrderDetails { get; set; }

    }
}
