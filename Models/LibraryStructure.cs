using System.ComponentModel.DataAnnotations;

namespace LibraryTRY3.Models
{
    public class LibraryStructure
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        public int Row { get; set; }

        [Required]
        public int Column { get; set; }
    }
}
