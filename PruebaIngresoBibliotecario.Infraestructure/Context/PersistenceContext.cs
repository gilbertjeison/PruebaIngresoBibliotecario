using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PruebaIngresoBibliotecario.Infraestructure.Entities;
using System.Threading.Tasks;

namespace PruebaIngresoBibliotecario.Infraestructure.Context
{
    public class PersistenceContext : DbContext
    {
        private readonly IConfiguration Config;

        public DbSet<Prestamo> Prestamo { get; set; }

        public PersistenceContext(DbContextOptions<PersistenceContext> options, IConfiguration config) : base(options)
        {
            Config = config;
        }

        public async Task CommitAsync()
        {
            await SaveChangesAsync().ConfigureAwait(false);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(Config.GetValue<string>("SchemaName"));

            base.OnModelCreating(modelBuilder);
        }
    }
}
