using Newtonsoft.Json;
using POS_Shop.Interfaces;
using POS_Shop.Models;
using POS_Shop.Models.LicenseModels;
using POS_Shop.Models.LicenseModels.DTO;
using POS_Shop.Repositories.LicenseServices;
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

            // 1. Check expiry
            if (DateTime.Now > licenseInfo.ExpiryDate)
                return false;

            // 2. Verify license key is valid (against hardcoded keys)
            if (!LicenseKeyManager.IsValidLicenseKey(licenseInfo.LicenseKey))
                return false;

            // 3. Check with database
            using (var context = new POSDbContext())
            {
                var license = context.Licenses
                    .FirstOrDefault(l => l.MacAddress == licenseInfo.MacAddress
                                        && l.HardwareId == licenseInfo.HardwareId
                                        && l.LicenseType == licenseInfo.LicenseType
                                        && l.IsActive);

                if (license == null)
                    return false;

                // Check if expired in database
                if (DateTime.Now > license.ExpiryDate)
                {
                    license.IsActive = false;
                    license.LastModifiedDate = DateTime.Now;
                    context.SaveChanges();
                    return false;
                }
            }

            // 4. Check if MAC address matches current system
            string currentMacAddress = _encryptionService.GetMacAddress();
            if (licenseInfo.MacAddress != currentMacAddress)
                return false;

            // 5. Check hardware ID
            string currentHardwareId = _encryptionService.GenerateHardwareId();
            if (licenseInfo.HardwareId != currentHardwareId)
                return false;

            return true;
        }

        public bool ActivateLicense(string userName, string licenseKey)
        {
            try
            {
                if (CheckLicenseFileExists())
                {
                    File.Delete(LicenseFileName);
                }
                // 1. Validate license key using hardcoded validation
                var validationResult = LicenseKeyManager.ValidateLicenseKey(licenseKey);
                if (!validationResult.IsValid)
                {
                    MessageBox.Show($"Invalid license key: {validationResult.ErrorMessage}",
                        "Activation Failed",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return false;
                }

                LicenseType licenseType = validationResult.LicenseType.Value;
                DateTime expiryDate = validationResult.ExpiryDate.Value;

                // 2. Get current hardware info
                string macAddress = _encryptionService.GetMacAddress();
                string hardwareId = _encryptionService.GenerateHardwareId();

                // 3. Check for existing license on this hardware
                using (var context = new POSDbContext())
                {
                    var activationCount= context.Licenses.Where(s=>s.MacAddress== macAddress
                    &&  s.LicenseType== licenseType && s.LicenseType==LicenseType.Trial).Count();
                    
                    if(activationCount>1)
                    {
                        MessageBox.Show("You have alreay avail the trail of this software. please contact to the support team to buy paid version", "Activation Successful",
                      MessageBoxButtons.OK, MessageBoxIcon.Information);

                        return false;
                    }
                    var existingLicense = context.Licenses
                        .FirstOrDefault(l => l.MacAddress == macAddress
                                            && l.HardwareId == hardwareId
                                            && l.IsActive);

                    if (existingLicense != null)
                    {
                        DialogResult result = MessageBox.Show(
                            "This computer already has an active license.\nDo you want to deactivate it and activate a new one?",
                            "Existing License Found",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question);

                        if (result != DialogResult.Yes)
                            return false;

                        // Deactivate existing license
                        existingLicense.IsActive = false;
                        existingLicense.LastModifiedDate = DateTime.Now;
                    }

                    // 4. Create new license record (NO LICENSE KEY STORED)
                    var newLicense = new AppLicense
                    {
                        UserName = userName,
                        MacAddress = macAddress,
                        HardwareId = hardwareId,
                        LicenseType = licenseType,
                        IssueDate = DateTime.Now,
                        ExpiryDate = expiryDate,
                        IsActive = true,
                        CreatedDate = DateTime.Now,
                        LastModifiedDate = DateTime.Now
                    };

                    context.Licenses.Add(newLicense);
                    context.SaveChanges();

                    // 5. Create license file (license key stored here only)
                    var licenseInfo = new LicenseInfo
                    {
                        UserName = userName,
                        LicenseKey = licenseKey, // Key stored in file, not DB
                        MacAddress = macAddress,
                        HardwareId = hardwareId,
                        LicenseType = licenseType,
                        IssueDate = DateTime.Now,
                        ExpiryDate = expiryDate,
                        IsValid = true
                    };

                    CreateLicenseFile(licenseInfo);

                    _currentLicense = licenseInfo;

                    // 6. Show success message
                    string message = $"✅ License Activated Successfully!\n\n" +
                                   $"👤 User: {userName}\n" +
                                   $"🔑 Type: {licenseType}\n" +
                                   $"📅 Expires: {expiryDate:dd/MM/yyyy}\n\n";

                    if (licenseType == LicenseType.Trial)
                    {
                        int days = (expiryDate - DateTime.Now).Days;
                        message += $"⏳ Trial Period: {days} days remaining";
                    }

                    MessageBox.Show(message, "Activation Successful",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Activation failed:\n{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public void CreateLicenseFile(LicenseInfo licenseInfo)
        {
            try
            {
                string jsonContent = JsonConvert.SerializeObject(licenseInfo, Formatting.Indented);
                string encryptedContent = _encryptionService.Encrypt(jsonContent);

                File.WriteAllText(LicenseFileName, encryptedContent);

                // Make file hidden
                File.SetAttributes(LicenseFileName,
                    File.GetAttributes(LicenseFileName) | FileAttributes.Hidden);
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

        public bool VerifyLicenseKeyInFile()
        {
            var licenseInfo = GetCurrentLicenseInfo();
            if (licenseInfo == null)
                return false;

            return LicenseKeyManager.IsValidLicenseKey(licenseInfo.LicenseKey);
        }
    }
}
