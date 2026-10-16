using System;

namespace POS_Shop.DTOs.Product
{
    public class ProductOrderHistoryDetails
    {
        public decimal Price { get; set; }
        public string TypeOfSale { get; set; }
        public string SaleDate { get; set; }
    }
}
