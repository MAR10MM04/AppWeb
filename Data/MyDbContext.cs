using Microsoft.EntityFrameworkCore;
using AppWeb.Models;

public class MyDbContext : DbContext
{
    public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Lote> Lotes { get; set; }
    public DbSet<Animal> Animales { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Relación User (1) -> Lote (muchos)
        modelBuilder.Entity<Lote>()
            .HasOne(l => l.User)
            .WithMany(u => u.Lotes) // Añade una colección en User si es necesario
            .HasForeignKey(l => l.Id_User);

        // Relación Lote (1) -> Animal (muchos)
        modelBuilder.Entity<Animal>()
            .HasOne(a => a.Lote)
            .WithMany(l => l.Animales)
            .HasForeignKey(a => a.Id_Lote);
    }
}