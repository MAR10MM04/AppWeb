using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppWeb.Models
{
    public class Lote
    {
        public int Id_Lote { get; set; }
        public int Id_User { get; set; }
        public int Id_Animal { get; set; }
        public string upp { get; set; }
        public string Nombre { get; set; }
        public int Remo { get; set; }
        public DateTime  Fecha_Entrada { get; set; }
        public DateTime Fecha_Salida { get; set; }
        public string Comunidad { get; set; }
    }

}