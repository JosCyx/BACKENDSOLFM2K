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

        //metodo que recibe como parametro un usuario y devuelve la lista de los roles asociados a ese usuario
        public List<RolUsuario> GetRoles(string login)
        {
            var roles = _context.RolUsuarios.Where(x => x.RuLogin == login).ToList();

            return roles;
        }
  

        //metodo que recibe una lista de roles y los busca en la tabla rolTransaccion y devuelve una lista de tipo RolTranscTemplate
        public List<RolTranscTemplate> GetRolTransaccion(List<RolUsuario> roles)
        {
            
            List<RolTranscTemplate> rolTransaccions = new List<RolTranscTemplate>();
            foreach (var rol in roles)
            {
                var rolTransaccion = _context.RolTransaccions.Where(x => x.RtRol == rol.RuRol).ToList();
                foreach (var rolTrans in rolTransaccion)
                {
                    RolTranscTemplate rolTranscTemplate = new RolTranscTemplate();
                    rolTranscTemplate.RolTMP = rolTrans.RtRol;
                    rolTranscTemplate.TranscTMP = rolTrans.RtTransaccion;
                    rolTransaccions.Add(rolTranscTemplate);
                }
            }

            //verifica que la lista de transacciones no tenga elementos con el mismo numero de TranscTMP
            var distinctRolTrans = rolTransaccions.GroupBy(x => x.TranscTMP).Select(y => y.First()).ToList();

            return distinctRolTrans;
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
