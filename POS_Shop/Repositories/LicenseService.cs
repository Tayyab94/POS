using Newtonsoft.Json;
using POS_Shop.Interfaces;
using POS_Shop.Models;
using POS_Shop.Models.LicenseModels;
using POS_Shop.Models.LicenseModels.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS_Shop.Repositories
{
    public class LicenseService : ILicenseService
    {
        private const string LicenseFileName = "license.lic";
        private readonly IEncryptionService _encryptionService;
        private LicenseInfo _currentLicense;

        public LicenseService()
        {
            _encryptionService = new EncryptionService();
        }

        public bool CheckLicenseFileExists()
        {
            return File.Exists(LicenseFileName);
        }

        public LicenseInfo ReadLicenseFile()
        {
            if (!CheckLicenseFileExists())
                return null;

            try
            {
                string encryptedContent = File.ReadAllText(LicenseFileName);
                string decryptedContent = _encryptionService.Decrypt(encryptedContent);

                var licenseInfo = JsonConvert.DeserializeObject<LicenseInfo>(decryptedContent);

               //licenseInfo.ExpiryDate = DateTime.Now.AddDays(-4);
                // Validate the license
                licenseInfo.IsValid = ValidateLicense(licenseInfo);

                _currentLicense = licenseInfo;
                return licenseInfo;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading license file: {ex.Message}");
                return null;
            }
        }

        public bool ValidateLicense(LicenseInfo licenseInfo)
        {
            if (licenseInfo == null)
                return false;

            // Check expiry
            if (DateTime.Now > licenseInfo.ExpiryDate)
                return false;

            // Validate with database using EF6
            using (var context = new POSDbContext())
            {
                var license = context.Licenses
                    .FirstOrDefault(l => l.LicenseKey == licenseInfo.LicenseKey
                                        && l.MacAddress == licenseInfo.MacAddress
                                        && l.HardwareId == licenseInfo.HardwareId
                                        && l.IsActive);

                if (license == null)
                    return false;

                // Check if expired
                if (DateTime.Now > license.ExpiryDate)
                {
                    // Update status if expired
                    license.IsActive = false;
                    license.LastModifiedDate = DateTime.Now;
                    context.SaveChanges();
                    return false;
                }
            }

            // Check if MAC address matches current system
            string currentMacAddress = _encryptionService.GetMacAddress();
            if (licenseInfo.MacAddress != currentMacAddress)
                return false;

            // Check hardware ID
            string currentHardwareId = _encryptionService.GenerateHardwareId();
            if (licenseInfo.HardwareId != currentHardwareId)
                return false;

            return true;
        }

        public bool ValidateLicenseKey(string licenseKey)
        {
            using (var context = new POSDbContext())
            {
                return context.Licenses.Any(l => l.LicenseKey == licenseKey);
            }
        }

        public LicenseType GetLicenseTypeFromKey(string licenseKey)
        {
            using (var context = new POSDbContext())
            {
                var license = context.Licenses.FirstOrDefault(l => l.LicenseKey == licenseKey);
                return license?.LicenseType ?? LicenseType.Trial;
            }
        }

        public DateTime CalculateExpiryDate(LicenseType licenseType)
        {
            DateTime _expire;
            switch(licenseType)
            {
                case LicenseType.Trial:
                   _expire= DateTime.Now.AddDays(15);
                    break;
                case LicenseType.OneYear:
                    _expire = DateTime.Now.AddYears(1);
                    break;
                case LicenseType.Lifetime:
                    _expire = DateTime.MaxValue;
                    break;
                default:
                    _expire = DateTime.Now.AddDays(15);
                    break;
            }

            return _expire;
        }

        public bool ActivateLicense(string userName, string licenseKey)
        {
            try
            {
                // Validate the key exists in database
                if (!ValidateLicenseKey(licenseKey))
                {
                    MessageBox.Show("Invalid license key!", "Activation Failed",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                // Get current hardware info
                string macAddress = _encryptionService.GetMacAddress();
                string hardwareId = _encryptionService.GenerateHardwareId();

                // Check if this hardware already has an active license
                using (var context = new POSDbContext())
                {
                    var existingLicense = context.Licenses
                        .FirstOrDefault(l => l.MacAddress == macAddress
                                            && l.HardwareId == hardwareId
                                            && l.IsActive);

                    if (existingLicense != null)
                    {
                        DialogResult result = MessageBox.Show(
                            "This computer already has an active license. Do you want to replace it?",
                            "Existing License Found",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question);

                        if (result != DialogResult.Yes)
                            return false;
                    }
                }

                // Get license type and create/update license in database
                using (var context = new POSDbContext())
                {
                    var license = context.Licenses.FirstOrDefault(l => l.LicenseKey == licenseKey);

                    if (license == null)
                    {
                        MessageBox.Show("License key not found in database!", "Activation Failed",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }

                    // Update license with current hardware info
                    license.UserName = userName;
                    license.MacAddress = macAddress;
                    license.HardwareId = hardwareId;
                    license.LicenseType = GetLicenseTypeFromKey(licenseKey);
                    license.IssueDate = DateTime.Now;
                    license.ExpiryDate = CalculateExpiryDate(license.LicenseType);
                    license.IsActive = true;
                    license.LastModifiedDate = DateTime.Now;

                    context.SaveChanges();

                    // Create license file
                    var licenseInfo = LicenseInfo.FromEntity(license);
                    CreateLicenseFile(licenseInfo);

                    _currentLicense = licenseInfo;

                    // Show success message
                    string message = $"License activated successfully!\n\n" +
                                   $"User: {userName}\n" +
                                   $"Type: {license.LicenseType}\n" +
                                   $"Expires: {license.ExpiryDate:dd/MM/yyyy}\n\n";

                    if (license.LicenseType == LicenseType.Trial)
                    {
                        message += $"Trial Period: 15 days";
                    }

                    MessageBox.Show(message, "Activation Successful",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Activation failed: {ex.Message}\n\nDetails: {ex.InnerException?.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public void CreateLicenseFile(LicenseInfo licenseInfo)
        {
            try
            {
                if (CheckLicenseFileExists())
                {
                    File.Delete(LicenseFileName);
                }

                string jsonContent = JsonConvert.SerializeObject(licenseInfo, Formatting.Indented);
                string encryptedContent = _encryptionService.Encrypt(jsonContent);

                File.WriteAllText(LicenseFileName, encryptedContent);

                // Hide the license file (optional)
                File.SetAttributes(LicenseFileName, File.GetAttributes(LicenseFileName) | FileAttributes.Hidden);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to create license file: {ex.Message}");
            }
        }

        public void DeleteLicenseFile()
        {
            if (CheckLicenseFileExists())
            {
                File.Delete(LicenseFileName);
            }
        }

        public bool IsLicenseValid()
        {
            if (!CheckLicenseFileExists())
                return false;

            var licenseInfo = ReadLicenseFile();
            return licenseInfo?.IsValid ?? false;
        }

        public LicenseInfo GetCurrentLicenseInfo()
        {
            if (_currentLicense == null)
            {
                ReadLicenseFile();
            }
            return _currentLicense;
        }

        public int GetRemainingDays()
        {
            var licenseInfo = GetCurrentLicenseInfo();
            if (licenseInfo == null || !licenseInfo.IsValid)
                return 0;

            var remaining = licenseInfo.ExpiryDate - DateTime.Now;
            return remaining.Days > 0 ? remaining.Days : 0;
        }
    }
}
