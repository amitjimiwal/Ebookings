using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Ebooking.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace Ebooking.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
        //The database reference we wrap it in the DbContext
        public DbSet<Events> Events { get; set; }
        public DbSet<EventCategory> EventCategories { get; set; }
        public DbSet<Bookings> Bookings { get; set; }
        public DbSet<TicketTypes> TicketTypes { get; set; }
        public DbSet<EventImage> EventImages { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ApplicationUser>(entity =>
            {
                entity.ToTable("Users");
                entity.Property(e => e.Id).HasColumnName("id"); ;
                entity.Property(e => e.UserName).HasColumnName("username").IsRequired();
                entity.Property(e => e.Email).HasColumnName("email").IsRequired();
                entity.Property(e => e.PhoneNumber).HasColumnName("phone_number").IsRequired();
                entity.Property(e => e.PasswordHash).HasColumnName("password").IsRequired();
                entity.Property(e => e.CreatedAt).HasColumnName("createdAt").HasDefaultValueSql("CURRENT_TIMESTAMP");
                //email and username should be unique
                entity.HasIndex(e => e.UserName).IsUnique();
                entity.HasIndex(e => e.Email).IsUnique();
            });
            
            // Embed Venue as a value object in the Event
            builder.Entity<Events>()
                .OwnsOne(e => e.Venue);

             builder.Entity<Events>()
                .HasMany(e => e.TicketTypes)
                .WithOne(t => t.Event); // Use the correct navigation property in TicketType  // Set explicit foreign key property

            // Configure relationships between Event and EventImage
            builder.Entity<Events>()
                .HasMany(e => e.EventImages)
                .WithOne(i => i.Event)  // Use the correct navigation property in EventImage
                .HasForeignKey(i => i.EventID);  // Set explicit foreign key property
        }
    }
}