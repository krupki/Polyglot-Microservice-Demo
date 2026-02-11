using PersonsApi.Models.DTOs;

namespace PersonsApi.Services;

public interface IPersonService
{
    Task<List<PersonResponseDto>> GetAllAsync();
    Task CreatePersonAsync(PersonCreateDto dto);
    Task<List<PersonResponseDto>> GetSortedFromGoAsync();
}