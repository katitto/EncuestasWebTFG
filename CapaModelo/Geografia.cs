using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaModelo
{
    public class Geografia
    {
        public int IdGeografia { get; set; }
        public string Pais { get; set; }
        public string CoordenadasX { get; set; }
        public string CoordenadasY { get; set; }
        public int Padre { get; set; }

    }
}
