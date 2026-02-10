using Microsoft.EntityFrameworkCore;
using PersonsApi.Entities;

namespace PersonsApi.Data
{
    public class PersonDbContext(DbContextOptions<PersonDbContext> options) : DbContext(options)
    {
        public DbSet<PersonEntity> Person { get; set; }
    }
}