using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaModelo
{
    public class Indicador
    {
        public int IdIndicador { get; set; }
        public string Descripcion { get; set; }
        public Unidad oUnidad { get; set; }
        public Tipo oTipo { get; set; }
        public Perfil oPerfil { get; set; }
    }
}
