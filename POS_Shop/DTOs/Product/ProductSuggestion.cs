using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Shop.DTOs.Product
{
    public class ProductSuggestion
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductUrduName { get; set; }

        public string ProductType { get; set; }
        public int Price { get; set; }
    }
}
