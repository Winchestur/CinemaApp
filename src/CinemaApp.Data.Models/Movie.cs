using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static CinemaApp.GCommon.EntityConstants;

namespace CinemaApp.Data.Models
{
    [Comment("Movie in the system")]
    public class Movie
    {
        [Comment("Movie Identifier")]
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Comment("Movie Title")]
        [Required(ErrorMessage = "Title is required.")]
        [MaxLength(MovieTitleMaxLength, 
            ErrorMessage = "Title cannot exceed {1} characters.")]
        public string Title { get; set; } = null!;

        [Comment("Movie Genre")]
        [Required(ErrorMessage = "Genre is required.")]
        [MaxLength(MovieGenreMaxLength, 
            ErrorMessage = "Genre cannot exceed {1} characters.")]
        public string Genre { get; set; } = null!;

        [Comment("Movie Release Date")]
        [Required]
        public DateTime ReleaseDate { get; set; }

        [Comment("Movie Director")]
        [Required(ErrorMessage = "Director is required.")]
        [MaxLength(MovieDirectorMaxLength, 
            ErrorMessage = "Director cannot exceed {1} characters.")]
        public string Director { get; set; } = null!;

        [Comment("Movie Duration in minutes")]
        [Range(DurationMinValue, DurationMaxValue, 
            ErrorMessage = "Duration must be between {1} and {2} minutes.")]
        public int Duration { get; set; }

        [Comment("Movie Description")]
        [Required(ErrorMessage = "Description is required.")]
        [MaxLength(MovieDescriptionMaxLength, 
            ErrorMessage = "Description cannot exceed {1} characters.")]
        public string Description { get; set; } = null!;

        [Comment("Movie Image URL")]
        [Required]
        [StringLength(UrlMaxLength, ErrorMessage = "UrlImages cannot exceed {1} characters.")]
        public string? ImageUrl { get; set; }

        public bool IsDeleted { get; set; } = false;

       //public virtual ICollection<CinemaMovie> MovieCinemas { get; set; }
       //    = new HashSet<CinemaMovie>();
       //
       //public virtual ICollection<ApplicationUserMovie> MovieApplicationUsers { get; set; }
       //    = new HashSet<ApplicationUserMovie>();
       //
       //public virtual ICollection<Ticket> Tickets { get; set; }
       //    = new HashSet<Ticket>();
    }
}