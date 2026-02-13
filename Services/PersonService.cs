using Grpc.Core;
using Grpc.Net.Client;
using Microsoft.EntityFrameworkCore;
using PersonsApi.Data;
using PersonsApi.Entities;
using PersonsApi.Models.DTOs;
using PersonsApi.Sorting;

namespace PersonsApi.Services;

public class PersonService(PersonDbContext context, IConfiguration configuration) : IPersonService
{
    private readonly Sorter.SorterClient sorterClient = new(
        GrpcChannel.ForAddress(
            configuration["SortingService:GrpcUrl"] ?? throw new InvalidOperationException("SortingService:GrpcUrl configuration is missing")));

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

        var request = new SortPersonsRequest();
        request.Persons.AddRange(persons.Select(p =>
            new Person { Id = p.Id, Name = p.Name, Age = p.Age }));

        try
        {
            var response = await sorterClient.SortPersonsAsync(request);
            return response.Persons
                .Select(p => new PersonResponseDto(p.Id, p.Name, p.Age))
                .ToList();
        }
        catch (RpcException)
        {
            return persons;
        }
    }
}