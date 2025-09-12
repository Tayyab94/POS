using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Shop.DTOs.City
{
    public class CitiesListForDataGridDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
      
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public bool IsActive { get; set; }
    }

    public class CustomerListForDataGridDto
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string ContactNo { get; set; }

        public int CityId { get; set; }
        public string CityName { get; set; }
        public bool IsDeleted { get; set; } = false;
    }

    public class SubListForDataGridDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public bool IsActive { get; set; }
    }
}
