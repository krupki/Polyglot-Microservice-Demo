using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using PersonsApi.Entities;

namespace PersonsApi.Data
{
    public class PersonDbContext(DbContextOptions<PersonDbContext> options) : DbContext(options)
    {
        public DbSet<PersonEntity> Person { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PersonEntity>().ToTable("person");
            modelBuilder.Entity<PersonEntity>().Property(p => p.Name).HasColumnName("name");
            modelBuilder.Entity<PersonEntity>().Property(p => p.Age).HasColumnName("age");
        }
    }
}