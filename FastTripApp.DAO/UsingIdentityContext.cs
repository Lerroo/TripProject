using FastTripApp.DAO.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using FastTripApp.DAO.Models.Identity;
using Microsoft.AspNetCore.Identity;
using System;

namespace FastTripApp.DAO
{
    public class UsingIdentityContext : IdentityDbContext<UserCustom>
    {
        public UsingIdentityContext(DbContextOptions<UsingIdentityContext> options): base(options)
        {
        }

        public DbSet<Trip> Trips { get; set; }
        public DbSet<HistoryTrip> HistoryTrips { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<TimeAfterDeparture> TimeAfterDepartures { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<UserCustom> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserCustom>()
            .ToTable("Users")
            .Property(p => p.Id).HasColumnName("UserId");

            builder.Entity<IdentityRole>()
                .ToTable("Roles");
        }
    }
}

