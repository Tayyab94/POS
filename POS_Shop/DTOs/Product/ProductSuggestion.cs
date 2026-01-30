using POS_Shop.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
        public int Qty { get; set; }
        public string purchasePrice { get; set; }
        public int Price { get; set; }
    }


    public class ProductListDtp
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UrduName { get; set; }

        public string SearchByName { get; set; }

        public string PurchasePrice { get; set; }
        public int Qty { get; set; }
        public int Cost { get; set; }
        
        public List<ProductPriceDTO> ProductPrices { get; set; }
    }

    public class ProductPriceDTO :ProdDTO
    {

        public string DisplayText =>
            $"Rs.{Price:0} per {Type} ({Items} pieces, Rs {P_Per_Item:0}/piece)";
    }


    public class ProdDTO
    {

        public string Type { get; set; }
        public int Items { get; set; }
        public decimal Price { get; set; }
        public decimal P_Per_Item { get; set; }
     
    }

}
