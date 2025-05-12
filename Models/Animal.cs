using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppWeb.Models
{
    public class Animal
    {
      
        public int Id_Animal { get; set; }
        public int Arete { get; set; }
        public int Peso { get; set; }
        public string Sexo { get; set; }      // Corregido: nombre correcto
        public string Clasificacion { get; set; } // Corregido: mayúscula
        public string Raza { get; set; }

        // FK a Lote
        public int Id_Lote { get; set; }      // Nueva propiedad para la relación
        public Lote Lote { get; set; }        // Propiedad de navegación
    }
}