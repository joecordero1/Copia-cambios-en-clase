using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Final_Cordero_Raura.Models;

namespace Final_Cordero_Raura.Data
{
    public class Final_Cordero_RauraContext : DbContext
    {
        public Final_Cordero_RauraContext (DbContextOptions<Final_Cordero_RauraContext> options)
            : base(options)
        {
        }

        public DbSet<Final_Cordero_Raura.Models.Pelicula> Pelicula { get; set; } = default!;

        public DbSet<Final_Cordero_Raura.Models.Resena>? Resena { get; set; }

        public DbSet<Final_Cordero_Raura.Models.Usuario>? Usuario { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pelicula>()
                .HasMany(i => i.Resenas)
                .WithOne(c => c.Pelicula)
                .HasForeignKey(c => c.IdPelicula);
        }
    }
}
