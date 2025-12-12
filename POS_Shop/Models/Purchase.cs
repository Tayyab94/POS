using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Shop.Models
{
    public class Purchase
    {
        public int Id { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string PurchaseNumber { get; set; }
        public string Reference { get; set; }
        public decimal TotalAmount { get; set; }

        // Foreign keys
        public int SupplierId { get; set; }

        // Navigation properties
        [ForeignKey("SupplierId")]
        public virtual Supplier Supplier { get; set; }

        public virtual ICollection<PurchaseDetail> PurchaseDetails { get; set; }
    }
}
