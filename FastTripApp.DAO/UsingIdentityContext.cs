using FastTripApp.DAO.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using FastTripApp.DAO.Models.Identity;

namespace FastTripApp.DAO
{
    public class UsingIdentityContext : IdentityDbContext<UsingIdentityUser>
    {
        public UsingIdentityContext(DbContextOptions<UsingIdentityContext> options)
            : base(options)
        {
        }

        public DbSet<Trip> Trips { get; set; }
        public DbSet<HistoryTrip> HistoryTrips { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Comment> Comments { get; set; }
       

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}

