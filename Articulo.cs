using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3TP4
{
    class Articulo
    {
        public long ID { get; set; }
        public DateTime FechaAlta { get; set; }
        public string Codigo { get; set; }
        public string Denominacion { get; set; }
        public double Precio { get; set; }
        public char Publicado { get; set; }
    }
}
