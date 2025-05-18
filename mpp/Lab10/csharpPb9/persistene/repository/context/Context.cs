using Microsoft.EntityFrameworkCore;

namespace persistene.repository.context
{
    public class Context : DbContext
    {
        string _connectionString;
        public DbSet<Arbitru> Arbitru { get; set; }
        public DbSet<Proba> Proba { get; set; }
        public DbSet<Participant> Participant { get; set; }

        public DbSet<Rezultat> Rezultat { get; set; }

        public Context(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite(_connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Proba>()
                 .Property(p => p.Categorie)
                 .HasConversion(
                     v => v.ToString(),
                     v => Enum.Parse<Categorie>(v)
                 );

        }
    }
}
