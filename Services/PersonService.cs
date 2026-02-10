using Microsoft.EntityFrameworkCore;
using PersonsApi.Data;
using PersonsApi.Models.DTOs;
using PersonsApi.Entities;

namespace PersonsApi.Services;

public class PersonService(PersonDbContext context) : IPersonService
{
    public async Task<List<PersonResponseDto>> GetAllAsync()
    {
        return await context.Person
            .Select(p => new PersonResponseDto(p.Id, p.Name, p.Age))
            .ToListAsync();
    }

    public async Task CreatePersonAsync(PersonCreateDto dto)
    {
        var entity = new PersonEntity { Name = dto.Name, Age = dto.Age };
        context.Person.Add(entity);
        await context.SaveChangesAsync();
    }
}