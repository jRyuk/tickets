using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Tickets.Domain.Entities;
using Tickets.Infrastructure.Identity;

namespace Tickets.Infrastructure.Pesistence
{
    public class ApplicationDbContext : IdentityDbContext<AppIdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //Creamos relaciones 
            builder.Entity<Ticket>().HasKey(c=> c.Id);
            builder.Entity<Ticket>().HasOne<AppIdentityUser>().WithMany()
                .HasForeignKey(c => c.UserId).OnDelete(DeleteBehavior.NoAction);

            builder.Entity<HistoryTicket>().HasKey(c => c.Id);
            builder.Entity<HistoryTicket>().HasOne(c => c.Ticket)
                .WithMany(c=> c.HistoryTickets).HasForeignKey(c=> c.TicketId);
        }

        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<HistoryTicket> HistoryTickets { get; set; }

    }
}
