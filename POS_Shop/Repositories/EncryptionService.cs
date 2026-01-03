using POS_Shop.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace POS_Shop.Repositories
{
    public class EncryptionService : IEncryptionService
    {
        private readonly byte[] _salt = Encoding.ASCII.GetBytes("YourSaltValue123");
        private const string EncryptionPassword = "YourStrongEncryptionPassword123!@#";


        public string Encrypt(string plainText)
        {
            if(string.IsNullOrEmpty(plainText))
                throw new ArgumentNullException(nameof(plainText));

            using(var algorithm =new RijndaelManaged())
            {

                using (var password = new Rfc2898DeriveBytes(EncryptionPassword, _salt))
                {
                    algorithm.Key= password.GetBytes(algorithm.KeySize / 8);
                    algorithm.IV = password.GetBytes(algorithm.BlockSize / 8);

                    using(var encryptor = algorithm.CreateEncryptor(algorithm.Key, algorithm.IV))
                    {
                        using(var ms = new System.IO.MemoryStream())
                        {
                            using(var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                            {
                                using(var sw = new System.IO.StreamWriter(cs))
                                {
                                    sw.Write(plainText);
                                }
                                return Convert.ToBase64String(ms.ToArray());
                            }
                        }
                    }
                }
            }
        }

        public string ComputeSHA256Hash(string input)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
                return BitConverter.ToString(bytes).Replace("-", "").ToLower();
            }
        }

        public string Decrypt(string cipherText)
        {
            if (string.IsNullOrEmpty(cipherText))
                throw new ArgumentNullException(nameof(cipherText));

            using (var algorithm = new RijndaelManaged())
            {
                using (var password = new Rfc2898DeriveBytes(EncryptionPassword, _salt))
                {
                    algorithm.Key = password.GetBytes(algorithm.KeySize / 8);
                    algorithm.IV = password.GetBytes(algorithm.BlockSize / 8);

                    using (var decryptor = algorithm.CreateDecryptor(algorithm.Key, algorithm.IV))
                    using (var ms = new MemoryStream(Convert.FromBase64String(cipherText)))
                    using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    using (var sr = new StreamReader(cs))
                    {
                        return sr.ReadToEnd();
                    }
                }
            }
        }


        public string GenerateHardwareId()
        {
            StringBuilder hardwareInfo = new StringBuilder();

            try
            {
                // Get Processor ID
                using (var searcher = new ManagementObjectSearcher("SELECT ProcessorId FROM Win32_Processor"))
                {
                    foreach (ManagementObject obj in searcher.Get())
                    {
                        hardwareInfo.Append(obj["ProcessorId"]?.ToString() ?? "");
                        break;
                    }
                }

                // Get Motherboard Serial Number
                using (var searcher = new ManagementObjectSearcher("SELECT SerialNumber FROM Win32_BaseBoard"))
                {
                    foreach (ManagementObject obj in searcher.Get())
                    {
                        hardwareInfo.Append(obj["SerialNumber"]?.ToString() ?? "");
                        break;
                    }
                }

                // Get Disk Serial Number
                using (var searcher = new ManagementObjectSearcher("SELECT SerialNumber FROM Win32_DiskDrive WHERE Index = 0"))
                {
                    foreach (ManagementObject obj in searcher.Get())
                    {
                        hardwareInfo.Append(obj["SerialNumber"]?.ToString() ?? "");
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                // Fallback to machine name if WMI fails
                hardwareInfo.Append(Environment.MachineName);
                hardwareInfo.Append(Environment.UserName);
            }

            return ComputeSHA256Hash(hardwareInfo.ToString());
        }

        public string GetMacAddress()
        {
            try
            {
                using (var searcher = new ManagementObjectSearcher("SELECT MACAddress FROM Win32_NetworkAdapter WHERE MACAddress IS NOT NULL AND PhysicalAdapter = TRUE"))
                {
                    foreach (ManagementObject obj in searcher.Get())
                    {
                        string mac = obj["MACAddress"]?.ToString();
                        if (!string.IsNullOrEmpty(mac))
                            return mac.Replace(":", "-");
                    }
                }
            }
            catch
            {
                // Fallback
                return "00-00-00-00-00-00";
            }
            return "00-00-00-00-00-00";
        }
    }
}
