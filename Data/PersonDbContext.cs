using Microsoft.Entity;
using PersonsApi.Entities;

namespace PersonsApi.Data
{
    public class PersonDbContext(DbContextOptions<PersonDbContext> options) : DbContext(options)
    {

    }
}