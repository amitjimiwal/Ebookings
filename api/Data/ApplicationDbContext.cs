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
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
        
        //The database reference we wrap it in the DbContext
        DbSet<Events> Events { get; set; }
        DbSet<EventCategory> EventCategories { get; set; }
        DbSet<Bookings> Bookings { get; set; }
    }
}