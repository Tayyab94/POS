using System.Collections.Generic;

namespace POS_Shop.Models
{
    public class Category
    {
        public int id { get; set; }
        public string name { get; set; }

        public ICollection<SubCategory> subCategory { get; set; }
    
        public bool isActive { get; set; }

    }
}
