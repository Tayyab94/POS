using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Shop.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [StringLength(maximumLength:50)]
        public string UserName { get; set; }
        [Required]
        [StringLength(maximumLength: 50)]
        public string UserPassword {  get; set; }
        public DateTime CreatedDate {  get; set; }

        public int RoleId { get; set; } 
        public virtual Role Role { get; set; }

        public bool IsValid(out List<ValidationResult> results)
        {
            var context = new ValidationContext(this);
            results = new List<ValidationResult>();
            return Validator.TryValidateObject(this, context, results, true);
        }
    }
}
