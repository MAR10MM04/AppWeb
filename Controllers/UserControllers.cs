using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using AppWeb.Data;
using AppWeb.Models;
using BCrypt.Net; // Agrega esta línea al inicio

namespace AppWeb.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly MyDbContext _context;

        public UsersController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/Users
              // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserResponseDto>>> GetUsers()
        {
            return await _context.Users
                .Select(u => new UserResponseDto
                {
                    Id_User = u.Id_User,
                    Name = u.Name,
                    Email = u.Email,
                    Upp = u.Upp
                })
                .ToListAsync();
        }

        // POST: api/Users
        [HttpPost]
        public async Task<ActionResult<UserResponseDto>> CreateUser([FromBody] CreateUserDto userDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = new User
            {
                Name = userDto.Name,
                Email = userDto.Email,
               Password = BCrypt.Net.BCrypt.HashPassword(userDto.Password), // Usa BCrypt
                Upp = userDto.Upp
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUser), new { id = user.Id_User }, new UserResponseDto
            {
                Id_User = user.Id_User,
                Name = user.Name,
                Email = user.Email,
                Upp = user.Upp
            });
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserResponseDto>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
                return NotFound();

            return new UserResponseDto
            {
                Id_User = user.Id_User,
                Name = user.Name,
                Email = user.Email,
                Upp = user.Upp
            };
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return NotFound();

            user.Name = updateDto.Name;
            user.Email = updateDto.Email;
            user.Upp = updateDto.Upp;

            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return NotFound();

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DTOs
        public class CreateUserDto
{
    [Required(ErrorMessage = "El nombre es obligatorio")]
    [StringLength(50, ErrorMessage = "Máximo 50 caracteres")]
    public required string Name { get; set; } // Usa "required"

    [Required(ErrorMessage = "El email es obligatorio")]
    [EmailAddress(ErrorMessage = "Formato inválido")]
    public required string Email { get; set; }

    [Required(ErrorMessage = "La contraseña es obligatoria")]
    [DataType(DataType.Password)]
    [MinLength(6, ErrorMessage = "Mínimo 6 caracteres")]
    public required string Password { get; set; }

    [StringLength(20, ErrorMessage = "Máximo 20 caracteres")]
    public string? Upp { get; set; } // Nullable
}

       public class UpdateUserDto
{
    [Required(ErrorMessage = "El nombre es obligatorio")]
    [StringLength(50, ErrorMessage = "Máximo 50 caracteres")]
    public required string Name { get; set; }

    [Required(ErrorMessage = "El email es obligatorio")]
    [EmailAddress(ErrorMessage = "Formato inválido")]
    public required string Email { get; set; }

    [StringLength(20, ErrorMessage = "Máximo 20 caracteres")]
    public string? Upp { get; set; } // Nullable
}
        public class UserResponseDto
        {
            public int Id_User { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public string Upp { get; set; }
        }
    }
}