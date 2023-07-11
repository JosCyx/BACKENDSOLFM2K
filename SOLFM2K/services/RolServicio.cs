using Microsoft.EntityFrameworkCore;
using SOLFM2K.Models;

namespace SOLFM2K.services
{
    public class RolServicio : IuService
    {

        private readonly  SolicitudContext _solicitudContext;

        public RolServicio(SolicitudContext solicitudContext)
        {
            _solicitudContext = solicitudContext;
        }

        public async Task<Rol> AddRolAsync(Rol rol)
        {
            try
            {
                await _solicitudContext.Rols.AddAsync(rol);
                await _solicitudContext.SaveChangesAsync();
                return rol;
            }
            catch { throw; }
        }

        public  Task<Rol> DeleteRolAsync(short ro_codigo)
        {
            return null;
        } 
        public async Task<List<Rol>> GetRolAsync()
        {
                try
                {
                    var Rols = await _solicitudContext.Rols.ToListAsync();
                return Rols;    
            }
                catch { throw; }
            }

        

        public async Task<Rol> GetRolbyIdAsync(short ro_codigo)
        {
            try
            {
                var Rols= await _solicitudContext.Rols.FindAsync(ro_codigo);
                return Rols;
            }
            catch
            { throw; }
        }

        public Task<Rol> UpdateRolAsync(short ro_codigo)
        {
            throw new NotImplementedException();
        }

        public Task<Rol> UpdateRolAsync(Rol rol)
        {
            throw new NotImplementedException();
        }
    }
}
