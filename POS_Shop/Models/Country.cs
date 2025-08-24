using System.Collections.Generic;

namespace POS_Shop.Models
{
    public class Country
    {
        public int Id { get; set; }

        public string CountryName { get; set; }

        public bool IsActive { get; set; }

        public ICollection<City> Cities { get; set; }
    }
}
