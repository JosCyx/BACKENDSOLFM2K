using SOLFM2K.Models;

namespace SOLFM2K.Services.CryptoService
{
    public interface ICryptoService
    {
        void EncryptPassword(string password);

        void DecryptPassword(string encryptedPassword);
    }
}
