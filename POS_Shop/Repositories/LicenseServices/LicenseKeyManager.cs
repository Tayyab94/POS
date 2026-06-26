using POS_Shop.Models.LicenseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace POS_Shop.Repositories.LicenseServices
{
    public static class LicenseKeyManager
    {
        // HARDCODED LICENSE KEYS - NEVER STORE IN DATABASE
        private static readonly Dictionary<string, LicenseType> ValidLicenseKeys =
            new Dictionary<string, LicenseType>(StringComparer.OrdinalIgnoreCase)
        {
            // Trial License - 15 days
            { "TRIAL-1234-5678-9012", LicenseType.Trial },
            
            // Yearly License - 1 year
            { "YEARLY-ABCD-EFGH-IJKL", LicenseType.OneYear },
            
            // Lifetime License - No expiration
            { "LIFETIME-MNOP-QRST-UVWX", LicenseType.Lifetime }
        };
       
        // Key format validation patterns
        private static readonly Dictionary<LicenseType, string> KeyPatterns =
            new Dictionary<LicenseType, string>
        {
            { LicenseType.Trial, @"^TRIAL-[A-Z0-9]{4}-[A-Z0-9]{4}-[A-Z0-9]{4}$" },
            { LicenseType.OneYear, @"^YEARLY-[A-Z0-9]{4}-[A-Z0-9]{4}-[A-Z0-9]{4}$" },
            { LicenseType.Lifetime, @"^LIFETIME-[A-Z0-9]{4}-[A-Z0-9]{4}-[A-Z0-9]{4}$" }
        };

        // Blacklisted/Revoked keys (empty by default)
        private static readonly HashSet<string> BlacklistedKeys = new HashSet<string>
        {
            // Add revoked keys here if needed
            // Example: "TRIAL-XXXX-XXXX-XXXX"
        };

        /// <summary>
        /// Check if a license key is valid
        /// </summary>
        public static bool IsValidLicenseKey(string licenseKey)
        {
            if (string.IsNullOrWhiteSpace(licenseKey))
                return false;

            string trimmedKey = licenseKey.Trim();

            // Check blacklist first
            if (BlacklistedKeys.Contains(trimmedKey))
                return false;

            return ValidLicenseKeys.ContainsKey(trimmedKey);
        }

        /// <summary>
        /// Get license type from key
        /// </summary>
        public static LicenseType? GetLicenseType(string licenseKey)
        {
            if (string.IsNullOrWhiteSpace(licenseKey))
                return null;

            string trimmedKey = licenseKey.Trim();

            if (ValidLicenseKeys.TryGetValue(trimmedKey, out LicenseType licenseType))
            {
                return licenseType;
            }

            return null;
        }

        /// <summary>
        /// Calculate expiry date based on license type
        /// </summary>
        public static DateTime CalculateExpiryDate(LicenseType licenseType)
        {
            switch (licenseType)
            {
                case LicenseType.Trial:
                    return DateTime.Now.AddDays(15);
                case LicenseType.OneYear:
                    return DateTime.Now.AddYears(1);
                case LicenseType.Lifetime:
                    return DateTime.MaxValue;
                default:
                    return DateTime.Now.AddDays(15);
            }
        }

        /// <summary>
        /// Validate key format
        /// </summary>
        public static bool ValidateKeyFormat(string licenseKey)
        {
            if (string.IsNullOrWhiteSpace(licenseKey))
                return false;

            string trimmedKey = licenseKey.Trim();

            // Get license type first
            var licenseType = GetLicenseType(trimmedKey);
            if (!licenseType.HasValue)
                return false;

            // Check pattern
            if (!KeyPatterns.TryGetValue(licenseType.Value, out string pattern))
                return false;

            return Regex.IsMatch(trimmedKey, pattern);
        }

        /// <summary>
        /// Comprehensive validation
        /// </summary>
        public static LicenseValidationResult ValidateLicenseKey(string licenseKey)
        {
            if (string.IsNullOrWhiteSpace(licenseKey))
            {
                return new LicenseValidationResult
                {
                    IsValid = false,
                    ErrorMessage = "License key is empty"
                };
            }

            string trimmedKey = licenseKey.Trim();

            // Check blacklist
            if (BlacklistedKeys.Contains(trimmedKey))
            {
                return new LicenseValidationResult
                {
                    IsValid = false,
                    ErrorMessage = "This license key has been revoked"
                };
            }

            // Check if key exists
            if (!ValidLicenseKeys.TryGetValue(trimmedKey, out LicenseType licenseType))
            {
                return new LicenseValidationResult
                {
                    IsValid = false,
                    ErrorMessage = "Invalid license key"
                };
            }

            // Validate format
            if (!ValidateKeyFormat(trimmedKey))
            {
                return new LicenseValidationResult
                {
                    IsValid = false,
                    ErrorMessage = "Invalid license key format"
                };
            }

            return new LicenseValidationResult
            {
                IsValid = true,
                LicenseType = licenseType,
                ExpiryDate = CalculateExpiryDate(licenseType)
            };
        }
    }

    public class LicenseValidationResult
    {
        public bool IsValid { get; set; }
        public string ErrorMessage { get; set; }
        public LicenseType? LicenseType { get; set; }
        public DateTime? ExpiryDate { get; set; }
    }
}
