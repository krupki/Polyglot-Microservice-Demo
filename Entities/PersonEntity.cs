using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonsApi.Entities
{
    public class PersonEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public long Id { get; set; }
        [Column("name")]
        public string Name { get; set; } = string.Empty;
        [Column("age")]
        public int Age { get; set; }
    }
}