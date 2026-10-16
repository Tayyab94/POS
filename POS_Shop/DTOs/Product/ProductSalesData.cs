using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Shop.DTOs.Product
{
    public class ProductSalesData
    {
        public string ProductName { get; set; }
        public decimal TotalQuantity { get; set; }
        public decimal TotalRevenue { get; set; }
        public int ProductId { get; set; }
        public decimal Percentage { get; set; }

        public string QuantityType { get; set; }
    }
}
