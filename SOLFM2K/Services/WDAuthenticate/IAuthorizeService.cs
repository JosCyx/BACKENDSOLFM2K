using Microsoft.EntityFrameworkCore;
using SOLFM2K.Models;
using System.Collections.Generic;

namespace SOLFM2K.Services.WDAuthenticate
{
    public interface IAuthorizeService
    {
        List<RolUsuario> GetRoles(string login);

        List<string> GetRolTransaccions(List<RolUsuario> roles);
    }
}
