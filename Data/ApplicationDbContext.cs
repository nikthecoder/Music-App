using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MusicApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Album> Album { get; set; }
        public DbSet<Band> Band { get; set; }
        public DbSet<Musician> Musician { get; set; }
        public DbSet<Review> Review { get; set; }
    }
}
