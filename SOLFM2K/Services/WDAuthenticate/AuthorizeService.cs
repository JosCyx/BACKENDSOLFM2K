using SOLFM2K.Models;
using System.Runtime.InteropServices;
using System.Security.Principal;


namespace SOLFM2K.Services.WDAuthenticate
{
    public class AuthorizeService : IAuthorizeService
    {
        private readonly SolicitudContext _context;

        public AuthorizeService(SolicitudContext context)
        {
            _context = context;
        }

        //modificar metodo para que retorne una lista de strings con los roles
        public List<RolUsuario> GetRoles(string login)
        {

            var roles = _context.RolUsuarios.Where(x => x.RuLogin == login).ToList();

            return roles;
        }


        public List<string> GetRolTransaccions(List<RolUsuario> roles)
        {
            List<string> transcTMPList = roles
        .SelectMany(rol => _context.RolTransaccions
            .Where(rolTrans => rolTrans.RtRol == rol.RuRol)
            .Select(rolTrans => rolTrans.RtTransaccion.ToString())) // Convierte a string
        .Distinct()
        .ToList();

            return transcTMPList;
        }

    }
}
