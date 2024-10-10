using System.ComponentModel.DataAnnotations;

namespace MvcLibrary.Models
{
    public class Book
    {
        public int Id { get; set; }

        [StringLength(120, MinimumLength = 3)]
        [Required]
        public string? Title { get; set; }

        [StringLength(240, MinimumLength = 3)]
        [Required]
        public string? Description { get; set; }

        [StringLength(120, MinimumLength = 3)]
        [Required]
        public string? Author { get; set; }


        [Display(Name = "Cover Image")]
        public string? CoverImage { get; set; }

        [StringLength(120, MinimumLength = 3)]
        [Required]
        public string? Publisher { get; set; }

        [Display(Name = "Publication Date")]
        [DataType(DataType.Date)]
        public DateTime PublicationDate { get; set; }

        [StringLength(120, MinimumLength = 3)]
        [Required]
        public string? Category { get; set; }

        [Range(1, 1000)]
        [Required]
        public int ISBN { get; set; }

        [Range(1, 1000)]
        [Required]
        [Display(Name = "Page Count")]
        public int PageCount { get; set; }

        [Required]
        [Display(Name = "Is Available?")]
        public bool IsAvailable { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; }

        [Display(Name = "User checked out")]
        public string? UserCheckedOut { get; set; }
        [Display(Name = "Due date")]
        public DateTime? DueDate { get; set; }
    }
}
