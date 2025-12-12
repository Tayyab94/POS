using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Shop.DTOs.Product
{
    public class ProductSalesData
    {
        public string ProductName { get; set; }
        public int TotalQuantity { get; set; }
        public double TotalRevenue { get; set; }
        public int ProductId { get; set; }
        public double Percentage { get; set; }

        public string QuantityType { get; set; }
    }
}
