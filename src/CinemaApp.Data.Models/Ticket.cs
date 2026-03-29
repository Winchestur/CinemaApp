using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.Data.Models
{
    public class Ticket
    {
        [Key]
        public Guid Id { get; set; }
        public decimal Price { get; set; }

        [Required]
        [ForeignKey(nameof(Cinema))]
        public Guid CinemaId { get; set; }
        public virtual Cinema Cinema { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(Movie))]
        public Guid MovieId { get; set; }
        public virtual Movie Movie { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(User))]
        public string UserId { get; set; } = null!;
        public IdentityUser User { get; set; } = null!;
    }
}
