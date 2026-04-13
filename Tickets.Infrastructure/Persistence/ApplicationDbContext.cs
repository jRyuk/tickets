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

            
        }

        public DbSet<Ticket> Tickets { get; set; }
      
    }
}
