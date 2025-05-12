using Microsoft.EntityFrameworkCore;

namespace AppWeb.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
        }

        // Your DbSet properties go here
    }
}