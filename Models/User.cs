using System.ComponentModel.DataAnnotations;

namespace LibraryTRY3.Models
{
    public class User
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        public string UserName { get; set; }

        [Required]
        [StringLength(100)]
        public string Class { get; set; }

        [StringLength(100)]
        public String Number { get; set; }

        [StringLength(100)]
        public string AdminName { get; set; }
    }
}
