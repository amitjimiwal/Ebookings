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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>(entity =>
            {
                entity.ToTable("Users");
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.UserName).HasColumnName("username").IsRequired();
                entity.Property(e => e.Email).HasColumnName("email").IsRequired();
                entity.Property(e => e.PhoneNumber).HasColumnName("phone_number").IsRequired();
                entity.Property(e => e.PasswordHash).HasColumnName("password").IsRequired();
                entity.Property(e => e.CreatedAt).HasColumnName("createdAt").HasDefaultValueSql("CURRENT_TIMESTAMP");
            });
        }
    }
}