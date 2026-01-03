namespace POS_Shop.Interfaces
{
    public interface IEncryptionService
    {
        string Encrypt(string plainText);
        string Decrypt(string cipherText);
        string ComputeSHA256Hash(string input);
        string GenerateHardwareId();
        string GetMacAddress();
    }
}
