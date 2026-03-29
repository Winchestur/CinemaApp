using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static CinemaApp.GCommon.EntityConstants;

namespace CinemaApp.Data.Models
{
    public class CinemaMovie
    {
        [Required]
        [ForeignKey(nameof(Cinema))]
        public Guid CinemaId { get; set; }
        public virtual Cinema Cinema { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(Movie))]
        public Guid MovieId { get; set; }
        public virtual Movie Movie { get; set; } = null!;

        [Required]
        public int AvailableTickets { get; set; }
        public bool IsDeleted { get; set; } = false;

        [StringLength(ShowTimesMaxLength)]
        public string ShowTimes { get; set; } = null!;
    }
}
