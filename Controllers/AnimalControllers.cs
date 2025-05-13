using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppWeb.Data;
using AppWeb.Models;
using AppWeb.DTOs;

namespace AppWeb.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnimalesController : ControllerBase
    {
        private readonly MyDbContext _context;

        public AnimalesController(MyDbContext context) => _context = context;

        // GET: api/Animales
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AnimalResponseDto>>> GetAnimales()
        {
            return await _context.Animales
                .Select(a => new AnimalResponseDto
                {
                    Id_Animal = a.Id_Animal,
                    Arete = a.Arete,
                    Peso = a.Peso,
                    Sexo = a.Sexo,
                    Clasificacion = a.Clasificacion,
                    Raza = a.Raza,
                    Id_Lote = a.Id_Lote
                })
                .ToListAsync();
        }

        // GET: api/Animales/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AnimalResponseDto>> GetAnimal(int id)
        {
            var animal = await _context.Animales
                .FirstOrDefaultAsync(a => a.Id_Animal == id);

            return animal == null ? NotFound() : new AnimalResponseDto
            {
                Id_Animal = animal.Id_Animal,
                Arete = animal.Arete,
                Peso = animal.Peso,
                Sexo = animal.Sexo,
                Clasificacion = animal.Clasificacion,
                Raza = animal.Raza,
                Id_Lote = animal.Id_Lote
            };
        }

// POST: api/Animales
        [HttpPost]
        public async Task<ActionResult<AnimalResponseDto>> CreateAnimal([FromBody] CreateAnimalDto animalDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            // Validar si el arete ya existe
            bool areteExiste = await _context.Animales
                .AnyAsync(a => a.Arete == animalDto.Arete);
            
            if (areteExiste)
                return BadRequest("El número de arete ya está registrado.");

            var lote = await _context.Lote.FindAsync(animalDto.Id_Lote);
            if (lote == null) return BadRequest("Lote no encontrado");

            var animal = new Animal
            {
                Arete = animalDto.Arete,
                Peso = animalDto.Peso,
                Sexo = animalDto.Sexo,
                Clasificacion = animalDto.Clasificacion,
                Raza = animalDto.Raza,
                Id_Lote = animalDto.Id_Lote
            };

            await _context.Animales.AddAsync(animal);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAnimal), new { id = animal.Id_Animal }, new AnimalResponseDto
            {
                Id_Animal = animal.Id_Animal,
                Arete = animal.Arete,
                Peso = animal.Peso,
                Sexo = animal.Sexo,
                Clasificacion = animal.Clasificacion,
                Raza = animal.Raza,
                Id_Lote = animal.Id_Lote
            });
        }

        // PUT: api/Animales/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAnimal(int id, [FromBody] UpdateAnimalDto updateDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var animal = await _context.Animales.FindAsync(id);
            if (animal == null) return NotFound();

            // Validar duplicado solo si se modifica el arete
            if (updateDto.Arete.HasValue && !string.IsNullOrEmpty(updateDto.Arete.Value.ToString()))
            {
                // Validar si el arete ya existe
                if (updateDto.Arete.HasValue)
                    updateDto.Arete = updateDto.Arete.Value;
                
                // Verificar si el nuevo arete ya existe en otro animal
                bool areteExiste = await _context.Animales
                    .AnyAsync(a => a.Arete == updateDto.Arete && a.Id_Animal != id);
                
                if (areteExiste)
                    return BadRequest("El número de arete ya está registrado.");
            }

            if (updateDto.Id_Lote.HasValue)
            {
                var lote = await _context.Lote.FindAsync(updateDto.Id_Lote.Value);
                if (lote == null) return BadRequest("Lote no encontrado");
            }

            // Actualizar propiedades solo si se proporcionan
            animal.Arete = updateDto.Arete ?? animal.Arete;
            animal.Peso = updateDto.Peso ?? animal.Peso;
            animal.Sexo = updateDto.Sexo ?? animal.Sexo;
            animal.Clasificacion = updateDto.Clasificacion ?? animal.Clasificacion;
            animal.Raza = updateDto.Raza ?? animal.Raza;
            animal.Id_Lote = updateDto.Id_Lote ?? animal.Id_Lote;

            _context.Entry(animal).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }


        // DELETE: api/Animales/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnimal(int id)
        {
            var animal = await _context.Animales.FindAsync(id);
            if (animal == null) return NotFound();

            _context.Animales.Remove(animal);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}