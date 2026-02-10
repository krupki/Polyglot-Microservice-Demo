using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonsApi.Models.DTOs
{
    public class PersonResponseDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public int Age { get; set; }
    }
}