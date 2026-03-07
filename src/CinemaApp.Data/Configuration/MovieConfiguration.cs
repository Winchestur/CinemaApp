using CinemaApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using static CinemaApp.GCommon.EntityConstants;

namespace CinemaApp.Data.Configuration
{
    public class MovieConfiguration : IEntityTypeConfiguration<Movie>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Movie> builder)
        {
            builder.HasKey(m => m.Id);

            builder.Property(m => m.Title)
                .IsRequired()
                .HasMaxLength(MovieTitleMaxLength);

            builder.Property(m => m.Genre)
                .IsRequired()
                .HasMaxLength(MovieGenreMaxLength);

            builder.Property(m => m.Director)
                .IsRequired()
                .HasMaxLength(MovieDirectorMaxLength);

            builder.Property(m => m.Description)
                .IsRequired()
                .HasMaxLength(MovieDescriptionMaxLength);

            builder.Property(m => m.Duration)
                .IsRequired()
                .HasDefaultValue(0);

            builder.Property(m => m.ImageUrl)
                .IsRequired()
                .HasMaxLength(UrlMaxLength);
        }
    }
}
