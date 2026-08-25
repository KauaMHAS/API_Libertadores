using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;

namespace ApiLibertadoresHAS.Extensions
{
    public static class ClaimTypesExtension
    {
        
        public static int UsuarioId(this ClaimsPrincipal user)
        {
            try
            {

                var usuarioId = user.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
                return int.Parse(usuarioId);
            }  
            catch
            {
                return 0;
            }
        }
    }
}