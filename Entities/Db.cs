using Microsoft.EntityFrameworkCore;
using Entities.Collections;
using System;

namespace Entities
{
    public class Db : DbContext
    {
        public Db(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Personas> personas { get; set; }
        public DbSet<Usuarios> usuarios { get; set; }
        public DbSet<Productos> productos { get; set; }
        public DbSet<Carros> carros { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<Personas>().HasOne(a => a.usuario).WithOne(b => b.persona).HasForeignKey<Usuarios>(b => b.idPersona);
            builder.Entity<Productos>().HasMany(c => c.carros).WithOne(e => e.producto).IsRequired().OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Usuarios>().HasMany(c => c.carros).WithOne(e => e.Usuario).IsRequired().OnDelete(DeleteBehavior.Restrict);

        }

    }
}
