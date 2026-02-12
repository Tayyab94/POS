namespace POS_Shop.DTOs.Supplier
{
    public class SupplierListForDataGridDto
    {
        public int Id { get; set; }
        public string SupplierName { get; set; }
        public string ShopName { get; set; }
        public string Address { get; set; }
        public string ContactNo { get; set; }

        public int CityId { get; set; }
        public string CityName { get; set; }
        public bool IsDeleted { get; set; } = false;
    }

}
