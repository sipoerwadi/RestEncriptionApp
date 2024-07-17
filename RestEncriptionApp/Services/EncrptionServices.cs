using System.Security.Cryptography;
using System.Text;

namespace RestEncriptionApp.Services
{
    public class EncrptionServices
    {
        private byte[] IV ={
                0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08,
                0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16
            };

        public string EncryprtData(string text, string prmKeyString,byte[] prmSalt)
        {
            using Aes aes = Aes.Create();
            aes.Key = DeriveKeyFromPassword(prmKeyString);
            //aes.IV = prmSalt;
            aes.IV = IV;
            using MemoryStream output = new();
            using CryptoStream cryptoStream = new(output, aes.CreateEncryptor(), CryptoStreamMode.Write);
            cryptoStream.Write(Encoding.Unicode.GetBytes(text));
            cryptoStream.FlushFinalBlock();
            return Convert.ToBase64String(output.ToArray());

        }


        private byte[] DeriveKeyFromPassword(string password)
        {
            var emptySalt = Array.Empty<byte>();
            var iterations = 1000;
            var desiredKeyLength = 16; // 16 bytes equal 128 bits.
            var hashMethod = HashAlgorithmName.SHA512;
            return Rfc2898DeriveBytes.Pbkdf2(Encoding.Unicode.GetBytes(password),
                                             emptySalt,
                                             iterations,
                                             hashMethod,
                                             desiredKeyLength);
        }
    }
}
