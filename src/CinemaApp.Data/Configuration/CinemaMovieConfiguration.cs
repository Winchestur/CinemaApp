using CinemaApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static CinemaApp.GCommon.EntityConstants;

namespace CinemaApp.Data.Configuration
{
    public class CinemaMovieConfiguration : IEntityTypeConfiguration<CinemaMovie>
    {
        public void Configure(EntityTypeBuilder<CinemaMovie> builder)
        {
            builder.HasKey(cm => new { cm.CinemaId, cm.MovieId });

            builder.Property(cm => cm.AvailableTickets)
                .IsRequired();

            builder.Property(cm => cm.IsDeleted)
                .HasDefaultValue(false);

            builder.Property(cm => cm.ShowTimes)
                .IsRequired()
                .HasMaxLength(ShowTimesMaxLength)
                .HasDefaultValue("00000");

            builder.HasOne(cm => cm.Cinema)
                .WithMany(c => c.CinemaMovies)
                .HasForeignKey(cm => cm.CinemaId);

            builder.HasOne(cm => cm.Movie)
                .WithMany(m => m.CinemaMovies)
                .HasForeignKey(cm => cm.MovieId);
        }
    }
}
