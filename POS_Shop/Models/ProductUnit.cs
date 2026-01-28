using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Shop.Models
{
    [Table("ProductUnits")]
    public class ProductUnit
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(20)]
        public string Name { get; set; }
        public string Abbreviation { get; set; }    

        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<ProductPrice> ProductPrices { get; set; }
    }
}
