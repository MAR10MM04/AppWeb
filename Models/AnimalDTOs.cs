using System.ComponentModel.DataAnnotations;

namespace AppWeb.DTOs
{
    // Para crear un animal
    public class CreateAnimalDto
    {
        [Required(ErrorMessage = "El número de arete es obligatorio")]
        [System.ComponentModel.DataAnnotations.Range(1, 999999)]
        public int Arete { get; set; }

        [Required(ErrorMessage = "El peso es obligatorio")]
        [System.ComponentModel.DataAnnotations.Range(1, 2000)]
        public int Peso { get; set; }

        [Required(ErrorMessage = "El sexo es obligatorio")]
        [StringLength(1)]
        public string Sexo { get; set; } = null!;

        [Required(ErrorMessage = "La clasificación es obligatoria")]
        [StringLength(50)]
        public string Clasificacion { get; set; } = null!;

        [Required(ErrorMessage = "La raza es obligatoria")]
        [StringLength(50)]
        public string Raza { get; set; } = null!;

        [Required(ErrorMessage = "El ID de lote es obligatorio")]
        public int Id_Lote { get; set; }
    }

    // Para actualizar un animal
    public class UpdateAnimalDto
    {
        [Range(1, 999999)]
        public int? Arete { get; set; }

        [Range(1, 2000)]
        public int? Peso { get; set; }

        [StringLength(1)]
        public string? Sexo { get; set; }

        [StringLength(50)]
        public string? Clasificacion { get; set; }

        [StringLength(50)]
        public string? Raza { get; set; }

        public int? Id_Lote { get; set; }
    }

    // Respuesta de un animal (sin ciclos)
    public class AnimalResponseDto
    {
        public int Id_Animal { get; set; }
        public int Arete { get; set; }
        public int Peso { get; set; }
        public string Sexo { get; set; } = null!;
        public string Clasificacion { get; set; } = null!;
        public string Raza { get; set; } = null!;
        public int Id_Lote { get; set; }
    }
}