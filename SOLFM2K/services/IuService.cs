using Microsoft.OpenApi.Any;
using SOLFM2K.Models;

namespace SOLFM2K.services
{
    public interface IuService
    {
        Task<List<Rol>> GetRolAsync();
        Task<Rol> GetRolbyIdAsync(short ro_codigo);
        Task<Rol> AddRolAsync(Rol rol);
        Task<Rol> UpdateRolAsync(short ro_codigo);
        Task<Rol> DeleteRolAsync(short ro_codigo);
        Task <Rol> UpdateRolAsync(Rol rol);
    }
}
