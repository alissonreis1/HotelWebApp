using HotelWebApp.Models;
using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;

namespace HotelWebApp.Data
{
    public class HotelContext : DbContext
    {
        public HotelContext(DbContextOptions<HotelContext> options) : base(options)
        {
        }

        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Quarto> Quartos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Quarto>()
                .HasOne(q => q.Hotel)
                .WithMany(h => h.Quartos)
                .HasForeignKey(q => q.HotelId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
