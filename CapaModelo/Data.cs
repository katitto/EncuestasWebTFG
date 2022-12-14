using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaModelo
{
    public class Data
    {
        public int IdData { get; set; }
        public int IdIndicador { get; set; }
        public int IdEncuesta { get; set; }
        public int IdPerfil { get; set; }
        public int IdEje{ get; set; }  
        public int Respuesta { get; set; }
        public string DescripcionEncuesta { get; set; }
        public  string DescripcionIndicador{ get; set; }
        public int Total { get; set; }
        public Indicador IdIndicadorAux { get; set; }


    }
}
