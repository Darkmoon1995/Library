using System.ComponentModel.DataAnnotations;

namespace LibraryTRY3.Models
{
    public class Admin
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Username { get; set; }

        [Required]
        [StringLength(100)]
        public string Password { get; set; }

        [Required]
        public bool Accepted { get; set; }
    }
}
