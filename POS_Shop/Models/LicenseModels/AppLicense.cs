using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Shop.Models.LicenseModels
{


    [Table("Licenses")]
    public class AppLicense
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string UserName { get; set; }

        // IMPORTANT: We DON'T store license key in database
        // Only store hardware info and license type

        [Required]
        [StringLength(50)]
        public string MacAddress { get; set; }

        [Required]
        [StringLength(200)]
        public string HardwareId { get; set; }

        [Required]
        public LicenseType LicenseType { get; set; }

        [Required]
        public DateTime IssueDate { get; set; }

        [Required]
        public DateTime ExpiryDate { get; set; }

        [Required]
        public bool IsActive { get; set; } = true;

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime LastModifiedDate { get; set; } = DateTime.Now;
    }
}
