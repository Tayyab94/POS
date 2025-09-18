using POS_Shop.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_Shop.DTOs.Order
{
    public class OrderDto: OrdersListDto
    {
        public int? CustomerId { get; set; }

        public List<OrderDetailDto> OrderDetailsList { get; set; }
    }

    public class OrderDetailDto
    {

        public int Id { get; set; }

        public int? ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public string QuantityType { get; set; }

        public float Price { get; set; }

    }
}
