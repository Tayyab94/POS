using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Shop.Models
{
    public class ProductPrice
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Prod_Unit_TypeId { get; set; }
        public string TypeName { get; set; }
        public string Unit { get; set; }
        public int ItemsCount { get; set; }
        public decimal Price { get; set; }
        public decimal PricePerItem { get; set; }
        public DateTime CreatedDate { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
        [ForeignKey("Prod_Unit_TypeId")]
        public virtual ProductUnit ProductUnitType { get; set; }
        // Helper property for UI
        public string DisplayText =>
            $"{TypeName}: ${Price:F2} per {Unit} ({ItemsCount} pieces, ${PricePerItem:F2}/piece)";
    }
}
