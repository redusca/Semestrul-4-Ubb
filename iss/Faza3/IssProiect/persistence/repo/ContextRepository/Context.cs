using Microsoft.EntityFrameworkCore;
using model;
using model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace persistence.repo.ContextRepository
{
    public class Context : DbContext
    {
        string _connectionString;
        public DbSet<User> Users { get; set; }
        public DbSet<Bug> Bugs { get; set; }

        public Context(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bug>()
                 .Property(b => b.Status)
                 .HasConversion(
                     v => v.ToString(),
                     v => Enum.Parse<BugStatus>(v)
                 );

            modelBuilder.Entity<User>()
                .Property(u => u.Type)
                .HasConversion(
                    v => v.ToString(),
                    v => Enum.Parse<UserType>(v)
                );
        }
    }
}
