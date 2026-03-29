using CinemaApp.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CinemaApp.Data.Configuration
{
    public class UserTicketsConfiguration : IEntityTypeConfiguration<UserTicket>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<UserTicket> builder)
        {
            builder.HasKey(t => t.Id);

            builder.HasKey(ut => new { ut.UserId, ut.TicketId });

            builder.HasOne(ut => ut.User)
                .WithMany()
                .HasForeignKey(ut => ut.UserId)
            .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ut => ut.Ticket)
                .WithMany()
                .HasForeignKey(ut => ut.TicketId)
            .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
