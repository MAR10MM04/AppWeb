using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppWeb.Data;
using AppWeb.Models;
using System.ComponentModel.DataAnnotations;

namespace AppWeb.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LotesController : ControllerBase
    {
        private readonly MyDbContext _context;

        public LotesController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/Lotes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LoteResponseDto>>> GetLotes()
        {
            return await _context.Lote
                .Include(l => l.User)
                .Include(l => l.Animales)
                .Select(l => new LoteResponseDto
                {
                    Id_Lote = l.Id_Lote,
                    Nombre = l.Nombre,
                    Remo = l.Remo,
                    Fecha_Entrada = l.Fecha_Entrada,
                    Fecha_Salida = l.Fecha_Salida,
                    Upp = l.Upp,
                    Comunidad = l.Comunidad,
                    Id_User = l.Id_User,
                    NombreUsuario = l.User.Name,
                    TotalAnimales = l.Animales.Count
                })
                .ToListAsync();
        }

        // GET: api/Lotes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LoteResponseDto>> GetLote(int id)
        {
            var lote = await _context.Lote
                .Include(l => l.User)
                .Include(l => l.Animales)
                .FirstOrDefaultAsync(l => l.Id_Lote == id);

            if (lote == null) return NotFound();

            return new LoteResponseDto
            {
                Id_Lote = lote.Id_Lote,
                Nombre = lote.Nombre,
                Remo = lote.Remo,
                Fecha_Entrada = lote.Fecha_Entrada,
                Fecha_Salida = lote.Fecha_Salida,
                Upp = lote.Upp,
                Comunidad = lote.Comunidad,
                Id_User = lote.Id_User,
                NombreUsuario = lote.User.Name,
                TotalAnimales = lote.Animales.Count
            };
        }

        // POST: api/Lotes
        [HttpPost]
        public async Task<ActionResult<LoteResponseDto>> CreateLote([FromBody] CreateLoteDto loteDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _context.Users.FindAsync(loteDto.Id_User);
            if (user == null)
                return BadRequest("Usuario no encontrado");

            var lote = new Lote
            {
                Id_User = loteDto.Id_User,
                Nombre = loteDto.Nombre,
                Remo = loteDto.Remo,
                Fecha_Entrada = loteDto.Fecha_Entrada,
                Fecha_Salida = loteDto.Fecha_Salida,
                Upp = loteDto.Upp,
                Comunidad = loteDto.Comunidad
            };

            await _context.Lote.AddAsync(lote);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetLote), new { id = lote.Id_Lote }, new LoteResponseDto
    {
        Id_Lote = lote.Id_Lote,
        Nombre = lote.Nombre,
        Remo = lote.Remo,
        Fecha_Entrada = lote.Fecha_Entrada,
        Fecha_Salida = lote.Fecha_Salida,
        Upp = lote.Upp,
        Comunidad = lote.Comunidad,
        Id_User = lote.Id_User,
        NombreUsuario = user.Name, // Asigna el nombre del usuario desde la relación
        TotalAnimales = 0 // Inicialmente 0 porque es un lote nuevo
    });
        }

        // PUT: api/Lotes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLote(int id, [FromBody] UpdateLoteDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var lote = await _context.Lote.FindAsync(id);
            if (lote == null)
                return NotFound();

            if (updateDto.Id_User.HasValue)
            {
                var user = await _context.Users.FindAsync(updateDto.Id_User.Value);
                if (user == null)
                    return BadRequest("Usuario no válido");
                lote.Id_User = updateDto.Id_User.Value;
            }

            lote.Nombre = updateDto.Nombre ?? lote.Nombre;
            lote.Remo = updateDto.Remo ?? lote.Remo;
            lote.Fecha_Entrada = updateDto.Fecha_Entrada ?? lote.Fecha_Entrada;
            lote.Fecha_Salida = updateDto.Fecha_Salida ?? lote.Fecha_Salida;
            lote.Upp = updateDto.Upp ?? lote.Upp;
            lote.Comunidad = updateDto.Comunidad ?? lote.Comunidad;

            _context.Entry(lote).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Lotes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLote(int id)
        {
            var lote = await _context.Lote
                .Include(l => l.Animales)
                .FirstOrDefaultAsync(l => l.Id_Lote == id);

            if (lote == null)
                return NotFound();

            if (lote.Animales.Any())
                return BadRequest("No se puede eliminar un lote con animales asociados");

            _context.Lote.Remove(lote);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DTOs
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
        }
    }
}
