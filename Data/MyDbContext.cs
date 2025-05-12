using Microsoft.EntityFrameworkCore;
using AppWeb.Models;
namespace AppWeb.Data
{
public class MyDbContext : DbContext
{
    public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Lote> Lote { get; set; }
    public DbSet<Animal> Animales { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configuración de PKs
            modelBuilder.Entity<User>()
                .HasKey(u => u.Id_User); // PK para User

            modelBuilder.Entity<Lote>()
                .HasKey(l => l.Id_Lote); // PK para Lote

            modelBuilder.Entity<Animal>()
                .HasKey(a => a.Id_Animal); // PK para Animal

        // Relación User (1) -> Lote (muchos)
        modelBuilder.Entity<Lote>()
            .HasOne(l => l.User)
            .WithMany(u => u.Lote) // Añade una colección en User si es necesario
            .HasForeignKey(l => l.Id_User);

        // Relación Lote (1) -> Animal (muchos)
        modelBuilder.Entity<Animal>()
            .HasOne(a => a.Lote)
            .WithMany(l => l.Animales)
            .HasForeignKey(a => a.Id_Lote);
    }
    }
}