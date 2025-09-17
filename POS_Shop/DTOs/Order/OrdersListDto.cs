using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Shop.DTOs.Order
{
    public class OrdersListDto
    {
        public int Id {  get; set; }

        public float TotalBill { get; set; }

        public float ReceiveAmount { get; set; }

        public DateTime CreatedDate { get; set; }

        public string InvoiceNumber { get; set; }

        public string paymentType { get; set; }
        public string CustomerName {  get; set; }
    }
}
