using Microsoft.EntityFrameworkCore;

namespace PruebaEQ_API.Models
{
    public class EQContext(DbContextOptions<EQContext> options) : DbContext(options)
    {
        public DbSet<DocKey> DocKeys {  get; set; }
        public DbSet<LogProcces> LogProcces {  get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<DocKey>().ToTable("DocKey");
            modelBuilder.Entity<LogProcces>().ToTable("LogProcces");
        }
    }

}
