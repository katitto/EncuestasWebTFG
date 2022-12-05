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
        public decimal CoordenadasX { get; set; }
        public decimal CoordenadasY { get; set; }
        public int Padre { get; set; }

    }
}
