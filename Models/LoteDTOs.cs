using System.ComponentModel.DataAnnotations;

namespace AppWeb.DTOs
{
    // Para crear un lote
    public class CreateLoteDto
    {
        [Required(ErrorMessage = "ID de usuario es obligatorio")]
        public int Id_User { get; set; }

        [Required(ErrorMessage = "Nombre es obligatorio")]
        [StringLength(50, MinimumLength = 3)]
        public string Nombre { get; set; } = null!;

        [Required(ErrorMessage = "Número de remo es obligatorio")]
        [Range(1, 1000)]
        public int Remo { get; set; }

        [Required(ErrorMessage = "Fecha de entrada es obligatoria")]
        public DateTime Fecha_Entrada { get; set; }

        public DateTime Fecha_Salida { get; set; }

        [Required(ErrorMessage = "UPP es obligatorio")]
        [StringLength(20)]
        public string Upp { get; set; } = null!;

        [Required(ErrorMessage = "Comunidad es obligatoria")]
        [StringLength(50)]
        public string Comunidad { get; set; } = null!;
    }

    // Para actualizar un lote
    public class UpdateLoteDto
    {
        public int? Id_User { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string? Nombre { get; set; }

        [Range(1, 1000)]
        public int? Remo { get; set; }

        public DateTime? Fecha_Entrada { get; set; }
        public DateTime? Fecha_Salida { get; set; }

        [StringLength(20)]
        public string? Upp { get; set; }

        [StringLength(50)]
        public string? Comunidad { get; set; }
    }

    // Respuesta de un lote (sin ciclos)
    public class LoteResponseDto
    {
        public int Id_Lote { get; set; }
        public string Nombre { get; set; } = null!;
        public int Remo { get; set; }
        public DateTime Fecha_Entrada { get; set; }
        public DateTime Fecha_Salida { get; set; }
        public string Upp { get; set; } = null!;
        public string Comunidad { get; set; } = null!;
        public int Id_User { get; set; }
        public string NombreUsuario { get; set; } = null!;
        public int TotalAnimales { get; set; }
        public List<AnimalResponseDto> Animales { get; set; } = new List<AnimalResponseDto>();
    }
}