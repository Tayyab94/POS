using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Shop.Models
{
    public class Supplier
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Supplier Name is Required")]
        [StringLength(100, ErrorMessage = "Supplier Name cannot exceed 100 characters")]
        public string SupplierName { get; set; }

        [StringLength(200, ErrorMessage = "Address cannot exceed 200 characters")]
        public string Address { get; set; }

        [StringLength(20, ErrorMessage = "Phone cannot exceed 20 characters")]
        public string Phone { get; set; }


        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        //public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<Purchase> Purchases { get; set; }

        public bool IsValid(out List<ValidationResult> results)
        {
            var context = new ValidationContext(this);
            results = new List<ValidationResult>();
            return Validator.TryValidateObject(this, context, results, true);
        }
    }
}
