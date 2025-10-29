using System;

namespace POS_Shop.DTOs.Order
{
    public class TempOrderListDto
    {

        public float TotalBill { get; set; }


        public DateTime CreatedDate { get; set; }

        public string InvoiceNumber { get; set; }


        public int? customerId { get; set; } = null;

        public string CustomerName { get; set; }

    }
}
