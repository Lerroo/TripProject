using FastTripApp.DAO.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using FastTripApp.DAO.Models.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using FastTripApp.DAO.Models.Trip;
using FastTripApp.DAO.Models.Trip.Way;
using FastTripApp.DAO.Models.Review;

namespace FastTripApp.DAO
{
    public class UsingIdentityContext : IdentityDbContext<UserCustom>
    {
        public UsingIdentityContext(DbContextOptions<UsingIdentityContext> options): base(options)
        {
        }

        public DbSet<DefaultTrip> Trips { get; set; }
        public DbSet<TimeBeforeDeparture> TimeBeforeDeparture { get; set; }
        public DbSet<DefaultWay> Ways { get; set; }
        public DbSet<Coords> Coords { get; set; }
        public DbSet<HistoryTrip> HistoryTrips { get; set; }
        public DbSet<DefaultReview> Reviews { get; set; }       
        public DbSet<Comment> Comments { get; set; }
        public DbSet<UserCustom> Users { get; set; }
        public DbSet<Place> Places { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {           
            base.OnModelCreating(builder);

            builder.Entity<UserCustom>()
            .ToTable("Users");

            builder.Entity<IdentityRole>()
                .ToTable("Roles");
        }
    }
}

