using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppWeb.Models
{
    public class Lote
    {
        
        public int Id_Lote { get; set; }
        public int Id_User { get; set; }       // FK a User
        public string Nombre { get; set; }
        public int Remo { get; set; }
        public DateTime Fecha_Entrada { get; set; }
        public DateTime Fecha_Salida { get; set; }
        public string Upp { get; set; }
        public string Comunidad { get; set; }

        // Propiedades de navegación
        public User User { get; set; }
        public ICollection<Animal> Animales { get; set; }
   
    }
}