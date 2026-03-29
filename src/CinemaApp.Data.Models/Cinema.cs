using System.ComponentModel.DataAnnotations;
using static CinemaApp.GCommon.EntityConstants;

namespace CinemaApp.Data.Models
{
    public class Cinema
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(CinemaNameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(CinemaLocationMaxLength)]
        public string Location { get; set; } = null!;
        public bool isDeleted { get; set; } = false;
        public virtual ICollection<CinemaMovie> CinemaMovies { get; set; } = new HashSet<CinemaMovie>();
        public virtual ICollection<Ticket> Tickets { get; set; } = new HashSet<Ticket>();
    }
}
