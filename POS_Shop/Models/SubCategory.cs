using System.ComponentModel.DataAnnotations.Schema;

namespace POS_Shop.Models
{
    public class SubCategory
    {
        public int id { get; set; }
        public string name { get; set; }
        [ForeignKey("categoryId")]
        public Category category { get; set; }
        public int categoryId { get; set; }
        public bool isActive { get; set; }

    }
}
