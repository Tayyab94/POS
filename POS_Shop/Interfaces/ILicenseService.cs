using POS_Shop.Models.LicenseModels;
using POS_Shop.Models.LicenseModels.DTO;

namespace POS_Shop.Interfaces
{
    public interface ILicenseService
    {
        bool CheckLicenseFileExists();
        LicenseInfo ReadLicenseFile();
        bool ValidateLicense(LicenseInfo licenseInfo);
        bool ActivateLicense(string userName, string licenseKey);
        void CreateLicenseFile(LicenseInfo licenseInfo);
        void DeleteLicenseFile();
        bool IsLicenseValid();
        LicenseInfo GetCurrentLicenseInfo();
        int GetRemainingDays();
        bool ValidateLicenseKey(string licenseKey);
        LicenseType GetLicenseTypeFromKey(string licenseKey);
    }
}
