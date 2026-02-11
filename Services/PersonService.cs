using Microsoft.EntityFrameworkCore;
using PersonsApi.Data;
using PersonsApi.Models.DTOs;
using PersonsApi.Entities;

namespace PersonsApi.Services;

public class PersonService(PersonDbContext context, IHttpClientFactory httpClientFactory) : IPersonService
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

    public async Task<List<PersonResponseDto>> GetSortedFromGoAsync()
    {
        var persons = await context.Person
            .Select(p => new PersonResponseDto(p.Id, p.Name, p.Age))
            .ToListAsync();

        var client = httpClientFactory.CreateClient();
        var response = await client.PostAsJsonAsync("http://localhost:8081/sort", persons);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<List<PersonResponseDto>>() ?? persons;
        }

        return persons;
    }
}