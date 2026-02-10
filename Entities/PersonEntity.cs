using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonsApi.Entities
{
    public class PersonEntity
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public int Age { get; set; }
    }
}