using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_Shop.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Product English Name is Required")]
        [StringLength(50, ErrorMessage = "English Name cannot exceed 50 characters")]
        public string ProductEnglishName { get; set; }
     
        [Required(ErrorMessage = "Product Urdu Name is Required")]
        [StringLength(50, ErrorMessage = "Urdu Name cannot exceed 50 characters")]
        public string ProductUrduName { get; set; }

        public string ProductType { get; set; }
        public decimal? PurchasePrice { get; set; }
        public decimal? SalePrice { get; set; }

        public int? Cost { get; set; }

        public int? SubcategoryId { get; set; }
        [ForeignKey("SubcategoryId")]
        public virtual SubCategory SubCategory { get; set; }

        public bool IsValid(out List<ValidationResult> results)
        {
            var context = new ValidationContext(this);
            results = new List<ValidationResult>();
            return Validator.TryValidateObject(this, context, results, true);
        }
    }
}
