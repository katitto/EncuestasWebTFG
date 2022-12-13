using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaModelo
{
    public class Resultado
    {
        public int IdEncuesta { get; set; } 
        public int IdEjePrincipal { get; set; }  //será un arraylist de un select donde el Peril se encuentra
        public int IdIndicador { get; set; }
        public int IdPerfil { get; set; }


    }
}
