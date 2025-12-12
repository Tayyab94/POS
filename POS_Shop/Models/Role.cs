using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Shop.Models
{
    public class Role
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string RoleName { get; set; }
       
        public virtual ICollection<User> Users { get; set; }

        public bool IsValid(out List<ValidationResult> results)
        {
            var context = new ValidationContext(this);
            results = new List<ValidationResult>();
            return Validator.TryValidateObject(this, context, results, true);
        }

    }
}
