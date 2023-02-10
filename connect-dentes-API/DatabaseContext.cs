using connect_dentes_API.Entities;
using Microsoft.EntityFrameworkCore;

namespace connect_dentes_API
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) 
        { 
        }

        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Atendimento> Atendimento { get; set; }
        public DbSet<Cliente> Cliente { get; set; }
    }
}
