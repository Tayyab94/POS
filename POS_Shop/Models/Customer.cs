using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Shop.Models
{
    public class Customer
    {

        public int Id { get; set; }

        [Required]
        public string CustomerName { get; set; }
        [Required]
        public string CustomerAddress { get; set; }
        [Required]
        public string ContactNo { get; set; }

        public int CityId { get; set; }
        [ForeignKey("CityId")]
        public City City { get; set; }

        public bool IsDeleted { get; set; } = false;

        public bool IsValid(out List<ValidationResult> results)
        {
            var context = new ValidationContext(this);
            results = new List<ValidationResult>();
            return Validator.TryValidateObject(this, context, results, true);
        }
    }
}
