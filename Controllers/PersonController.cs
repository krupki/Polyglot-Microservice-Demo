using Microsoft.AspNetCore.Mvc;
using PersonsApi.Models.DTOs;
using PersonsApi.Services;

namespace PersonsApi.Controllers;

[ApiController]
[Route("api/personen")]
public class PersonController(IPersonService personService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<PersonResponseDto>>> GetAll()
    {
        var persons = await personService.GetAllAsync();
        return Ok(persons);
    }

    [HttpPost]
    public async Task<ActionResult> Create(PersonCreateDto dto)
    {
        await personService.CreatePersonAsync(dto);
        return StatusCode(201);
    }

    [HttpGet("sorted")]
    public async Task<ActionResult<List<PersonResponseDto>>> GetSorted()
    {
        var sortedPersons = await personService.GetSortedFromGoAsync();
        return Ok(sortedPersons);
    }
}