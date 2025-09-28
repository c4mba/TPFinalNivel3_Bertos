using dominio;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace negocio
{
    public static class Seguridad
    {
        public static bool sesionActiva(object usuario)
        {
            User user = usuario != null ? (User)usuario : null;
            if (usuario != null && user.Id != 0)
            {
                return true;
            }
            return false;
        }
        public static bool esAdmin(object usuario)
        {
            User user = usuario != null ? ( User)usuario : null;    
            return user != null ? user.esAdmin : false;
        }
    }
}
