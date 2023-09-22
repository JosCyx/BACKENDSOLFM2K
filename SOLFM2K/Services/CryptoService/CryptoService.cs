using System.Security.Cryptography;
using System.Text;

namespace SOLFM2K.Services.CryptoService
{
    public class CryptoService
    {
        private readonly string encryptionKey; // ESTABLECER UNA CLAVE DE CIRFRADO SEGURA

        public CryptoService(string encryptionKey)
        {
            this.encryptionKey = encryptionKey;
        }

        public string EncryptPassword(string password)
        {
            using Aes aesAlg = Aes.Create();

            aesAlg.Key = Encoding.UTF8.GetBytes(encryptionKey);
            aesAlg.IV = new byte[16]; 

            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            using MemoryStream msEncrypt = new MemoryStream();
            using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
            using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
            {
                swEncrypt.Write(password);
            }

            return Convert.ToBase64String(msEncrypt.ToArray());
        }

        public string DecryptPassword(string encryptedPassword)
        {
            using Aes aesAlg = Aes.Create();

            aesAlg.Key = Encoding.UTF8.GetBytes(encryptionKey);
            aesAlg.IV = new byte[16]; 

            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            using MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(encryptedPassword));
            using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
            using (StreamReader srDecrypt = new StreamReader(csDecrypt))
            {
                return srDecrypt.ReadToEnd();
            }
        }
    }
}
