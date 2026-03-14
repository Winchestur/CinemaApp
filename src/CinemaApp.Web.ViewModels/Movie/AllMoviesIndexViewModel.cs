using System.ComponentModel.DataAnnotations;
using static CinemaApp.GCommon.EntityConstants;

namespace CinemaApp.Web.ViewModels.Movie
{
    public class AllMoviesIndexViewModel
    {
        [Key]
        public string Id { get; set; } = null!;

        [Required]
        [StringLength(TitleMaxLength, MinimumLength = TitleMinLength)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(GenreMaxLength, MinimumLength = GenreMinLength)]
        public string Genre { get; set; } = null!;

        [Required]
        public string ReleaseDate { get; set; } = null!;

        [Required]
        [StringLength(DirectorNameMaxLength, MinimumLength = DirectorNameMinLength)]
        public string Director { get; set; } = null!;

        [Required]
        public int Duration { get; set; }

        [Required]
        [StringLength(ImageUrlMaxLength)]
        public string? ImageUrl { get; set; }
    }
}
