using SOLFM2K.Models;

namespace SOLFM2K.Services.CryptoService
{
    public interface ICryptoService
    {
        string EncryptPassword(string password);

        string DecryptPassword(string encryptedPassword);
    }
}
