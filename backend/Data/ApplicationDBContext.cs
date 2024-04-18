using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace backend.Data
{
    public class ApplicationDBContext : IdentityDbContext<Utilisateur>
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<Entrepot> Entrepot { get; set; }
        public DbSet<Utilisateur> Utilisateur { get; set; }
        public DbSet<Vehicule> Vehicule { get; set; }
        //public DbSet<TypeVoyage> TypeVoyage { get; set; }
        //public DbSet<Voyage> Voyage { get; set; }
        //public DbSet<UtilisateurVoyage> UtilisateurVoyage { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            List<IdentityRole> roles = new List<IdentityRole>{
                new IdentityRole{
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole{
                    Name = "Magasinier",
                    NormalizedName = "MAGASINIER"
                },
                new IdentityRole{
                    Name="Demandeur",
                    NormalizedName = "DEMANDEUR"
                },
                new IdentityRole{
                    Name="Transporteur",
                    NormalizedName = "TRANSPORTEUR"
                },
            };
            builder.Entity<IdentityRole>().HasData(roles);
    //         builder.Entity<Voyage>()
    //         .HasMany(v => v.Utilisateurs)
    //         .WithMany(u => u.Voyages)
    //         .UsingEntity(j => j.ToTable("UtilisateurVoyage"));

    //         builder.Entity<UtilisateurVoyage>()
    //           .HasKey(uv => new { uv.UtilisateursID, uv.VoyagesId });
    //         builder.Entity<Voyage>()
    //   .HasOne(e => e.Magasinier)
    //   .WithOne(e => e.Voyage)
    //   .HasForeignKey<Voyage>(e => e.IdVoyage)
    //   .IsRequired();
        }
    }
}