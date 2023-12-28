using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using Data.Models;

namespace Data
{
    public class ProiectPsscDb : DbContext
    {
        public ProiectPsscDb()
        {
        }
        public ProiectPsscDb(DbContextOptions<ProiectPsscDb> options) : base(options)
        {
        }
        public DbSet<ProductDTO> Products { get; set; }

        public DbSet<OrderHeaderDTO> OrderHeaders { get; set; }

        public DbSet<OrderLineDTO> OrderLines { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductDTO>().ToTable("Products").HasKey(p => p.ProductId);
            modelBuilder.Entity<OrderHeaderDTO>().ToTable("OrderHeaders").HasKey(oh => oh.OrderHeaderId);
            modelBuilder.Entity<OrderLineDTO>().ToTable("OrderLines").HasKey(ol => ol.OrderLineId);
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost;Database=PsscDb;Trusted_Connection=True;TrustServerCertificate=True");
            }
        }
    }
}


