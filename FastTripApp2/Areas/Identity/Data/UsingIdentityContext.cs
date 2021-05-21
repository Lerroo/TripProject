using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FastTripApp2.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UsingIdentity.Areas.Identity.Data;

namespace UsingIdentity.Data
{
    public class UsingIdentityContext : IdentityDbContext<UsingIdentityUser>
    {
        public UsingIdentityContext(DbContextOptions<UsingIdentityContext> options)
            : base(options)
        {
        }

        public DbSet<Trip> Trips { get; set; }
        public DbSet<HistoryTrip> HistoryTrips { get; set; }
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
