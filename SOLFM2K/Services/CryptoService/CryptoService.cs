using System.Security.Cryptography;
using System.Text;

namespace SOLFM2K.Services.CryptoService
{
    public class CryptoService : ICryptoService
    {
        private readonly string encryptionKey; // ESTABLECER UNA CLAVE DE CIFRADO SEGURA

        public CryptoService(string encryptionKey)
        {
            this.encryptionKey = encryptionKey;
        }
       

        public string EncryptPassword(string password)
        {
            using Aes aesAlg = Aes.Create();

            aesAlg.Key = Encoding.UTF8.GetBytes(encryptionKey);

            // Generar un IV aleatorio
            aesAlg.GenerateIV();
            byte[] iv = aesAlg.IV; // Obtener el IV aleatorio generado

            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            using MemoryStream msEncrypt = new MemoryStream();
            using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
            using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
            {
                swEncrypt.Write(password);
            }

            // Obtener los datos cifrados
            byte[] encryptedData = msEncrypt.ToArray();

            // Concatenar el IV al principio de los datos cifrados
            byte[] result = new byte[iv.Length + encryptedData.Length];
            Buffer.BlockCopy(iv, 0, result, 0, iv.Length);
            Buffer.BlockCopy(encryptedData, 0, result, iv.Length, encryptedData.Length);

            // Convertir el resultado final en Base64
            return Convert.ToBase64String(result);
        }

        public string DecryptPassword(string encryptedPassword)
        {
            using Aes aesAlg = Aes.Create();

            aesAlg.Key = Encoding.UTF8.GetBytes(encryptionKey);

            // Extraer el IV del principio de los datos cifrados
            byte[] encryptedData = Convert.FromBase64String(encryptedPassword);
            byte[] iv = new byte[16]; // El IV tiene una longitud fija de 16 bytes
            Buffer.BlockCopy(encryptedData, 0, iv, 0, iv.Length);

            aesAlg.IV = iv;

            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            using MemoryStream msDecrypt = new MemoryStream(encryptedData, iv.Length, encryptedData.Length - iv.Length);
            using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
            using (StreamReader srDecrypt = new StreamReader(csDecrypt))
            {
                return srDecrypt.ReadToEnd();
            }
        }
    }

    /*public class CryptoService : ICryptoService
    {
        private readonly string encryptionKey = "fm2k"; // ESTABLECER UNA CLAVE DE CIRFRADO SEGURA

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

       
    }*/
}

