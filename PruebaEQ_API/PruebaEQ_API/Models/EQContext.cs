using Microsoft.EntityFrameworkCore;

namespace PruebaEQ_API.Models
{
    // Clase de contexto principal de Entity Framework.
    // Aquí se definen las entidades que representarán las tablas de la base de datos.

    public class EQContext(DbContextOptions<EQContext> options) : DbContext(options)
    {
        public DbSet<DocKey> DocKeys { get; set; }             // Tabla de claves de documentos
        public DbSet<LogProcess> LogProcces { get; set; }      // Tabla de registros de proceso
        public DbSet<User> Users { get; set; }                 // Tabla de usuarios

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configura los nombres exactos de las tablas
            modelBuilder.Entity<DocKey>().ToTable("DocKey");
            modelBuilder.Entity<LogProcess>().ToTable("LogProcess");
            modelBuilder.Entity<User>().ToTable("User");
        }
    }


}
