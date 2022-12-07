using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaModelo
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string User { get; set; }
        public string Contrasena { get; set; }
        public string Email { get; set; }
        public EjePrincipal oEje { get; set; }
        public Rol oRol { get; set; }


    }
}
