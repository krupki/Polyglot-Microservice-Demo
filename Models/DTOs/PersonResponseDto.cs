namespace PersonsApi.Models.DTOs
{
    public class PersonResponseDto(long id, string name, int age)
    {
        public long Id { get; init; } = id;
        public string Name { get; init; } = name;
        public int Age { get; init; } = age;
    }
}