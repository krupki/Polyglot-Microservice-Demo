namespace PersonsApi.Models.DTOs
{
    public class PersonCreateDto
    {
        public required string Name { get; set; }
        public int Age { get; set; }
    }
}