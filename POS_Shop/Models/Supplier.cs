using POS_Shop.Models.Suppliers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_Shop.Models
{
    public class Supplier
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        
        [StringLength(maximumLength: 50, ErrorMessage = "Supplier code cannot exceed 50 characters.")]
        [Required]
        public string SupplierName { get; set; }
        
        [Required]
        [StringLength(maximumLength: 100, ErrorMessage = "Shop name cannot exceed 100 characters.")]
        public string ShopName { get; set; }

        [StringLength(maximumLength: 100, ErrorMessage = "Shop name cannot exceed 100 characters.")]
        [Required]
        public string Address { get; set; }
        [Required]
        [StringLength(maximumLength: 20, ErrorMessage = "Contact cannot exceed 20 characters.")]
        public string ContactNo { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid city")]
        public int CityId { get; set; }
        [ForeignKey("CityId")]
        public City City { get; set; }

        public bool IsDeleted { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool IsValid(out List<ValidationResult> results)
        {
            var context = new ValidationContext(this);
            results = new List<ValidationResult>();
            return Validator.TryValidateObject(this, context, results, true);
        }

        
        public virtual ICollection<Purchase> Purchases { get; set; }

        public virtual ICollection<SupplierPayment> SupplierPayments { get; set; }
    }
}
