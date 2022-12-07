using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaModelo
{
    public class EjePrincipal
    {
        public int IdEje { get; set; }
        public string RefEje { get; set; }
        public int Nivel { get; set; }
        public string Nombre { get; set; }
        public int IdEjePadre { get; set; }
        public Perfil oPerfil { get; set; }//¿tendría que ser del tipo rol? sí
        public Geografia oGeografia { get; set; }//¿tendría que ser del tipo geografia? sí
    }
}
