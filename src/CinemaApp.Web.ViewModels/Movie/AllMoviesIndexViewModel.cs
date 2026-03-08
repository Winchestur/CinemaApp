using System.ComponentModel.DataAnnotations;
using static CinemaApp.GCommon.EntityConstants;

namespace CinemaApp.Web.ViewModels.Movie
{
    public class AllMoviesIndexViewModel
    {
        [Key]
        public string Id { get; set; } = null!;

        [Required]
        [StringLength(MovieTitleMaxLength, MinimumLength = MovieTitleMinLength)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(MovieGenreMaxLength, MinimumLength = MovieGenreMinLength)]
        public string Genre { get; set; } = null!;

        [Required]
        public string ReleaseDate { get; set; } = null!;

        [Required]
        [StringLength(MovieDirectorMaxLength, MinimumLength = MovieDirectorMinLength)]
        public string Director { get; set; } = null!;

        [Required]
        [StringLength(DurationMaxValue, MinimumLength = DurationMinValue)]
        public string Duration { get; set; } = null!;

        [Required]
        [StringLength(UrlMaxLength, MinimumLength = UrlMinLength)]
        public string? ImageUrl { get; set; }
    }
}
