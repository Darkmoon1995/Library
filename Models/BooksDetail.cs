using System.ComponentModel.DataAnnotations;

namespace LibraryTRY3.Models
{
    public class BooksDetail
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        public string Location { get; set; }

        [Required]
        public bool Taken { get; set; }

        [StringLength(100)]
        public string? TakenBy { get; set; }

        [StringLength(100)]
        public string? Author { get; set; }

        [StringLength(100)]
        public string? Publisher { get; set; }

        public int YearOfPublication { get; set; }

        [Required]
        [StringLength(100)]
        public string UserAdminAdded { get; set; }
    }
}
