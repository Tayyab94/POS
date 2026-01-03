using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Shop.Models.LicenseModels.DTO
{
    public class LicenseInfo
    {
        public string UserName { get; set; }
        public string LicenseKey { get; set; }
        public string MacAddress { get; set; }
        public string HardwareId { get; set; }
        public LicenseType LicenseType { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool IsValid { get; set; }

        public LicenseInfo()
        {
            IssueDate = DateTime.Now;
        }

        public static LicenseInfo FromEntity(AppLicense entity)
        {
            return new LicenseInfo
            {
                UserName = entity.UserName,
                LicenseKey = entity.LicenseKey,
                MacAddress = entity.MacAddress,
                HardwareId = entity.HardwareId,
                LicenseType = entity.LicenseType,
                IssueDate = entity.IssueDate,
                ExpiryDate = entity.ExpiryDate
            };
        }
    }
}
