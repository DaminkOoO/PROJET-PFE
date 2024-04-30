using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using backend.Interfaces;
using backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace backend.Data
{
    public class ApplicationDBContext : IdentityDbContext<IdentityUser>
    {

        public ApplicationDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<Entrepot> Entrepot { get; set; }
        public DbSet<Magasinier> Magasinier { get; set; }
        public DbSet<Transporteur> Transporteur { get; set; }
        //public DbSet<TransporteurVoyage> TransporteurVoyage { get; set; }
        public DbSet<Demandeur> Demandeur { get; set; }
        public DbSet<Vehicule> Vehicule { get; set; }
        public DbSet<TypeVoyage> TypeVoyage { get; set; }
        public DbSet<Voyage> Voyage { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //builder.Entity<TransporteurVoyage>().HasKey(t => new { t.Transporteur, t.VoyageId });
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


            builder.Entity<Voyage>().HasOne(p => p.Magasinier).WithOne().HasForeignKey<Magasinier>(p => p.MatriculeSopal);
            builder.Entity<Demandeur>().HasOne(p => p.Utilisateur).WithOne().HasForeignKey<Demandeur>(p => p.UserId);
            builder.Entity<Magasinier>().HasOne(p => p.Utilisateur).WithOne().HasForeignKey<Magasinier>(p => p.MatriculeSopal);
            builder.Entity<Transporteur>().HasOne(p => p.Utilisateur).WithOne().HasForeignKey<Transporteur>(p => p.UserId);
            builder.Entity<Transporteur>()
            .HasOne(t => t.Vehicule)
            .WithMany()
            .HasForeignKey(t => t.VehiculeMatricule)
            .HasPrincipalKey(v => v.Matricule)
            .OnDelete(DeleteBehavior.NoAction);
            builder.Entity<Demandeur>().HasMany(d => d.Voyages).WithOne(v => v.Demandeur).HasForeignKey(v => v.DemandeurMatriculeSopal).OnDelete(DeleteBehavior.NoAction);
            builder.Entity<Magasinier>().HasMany(d => d.Voyages).WithOne(v => v.Magasinier).HasForeignKey(v => v.MagasinierMatriculeSopal).OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Transporteur>()
  .HasMany(t => t.Voyages)
  .WithMany(v => v.Transporteurs)
  .UsingEntity<TransporteurVoyage>(
     l => l.HasOne<Voyage>().WithMany().HasForeignKey(e => e.VoyageId).OnDelete(DeleteBehavior.NoAction),
            r => r.HasOne<Transporteur>().WithMany().HasForeignKey(e => e.TransporteurMatriculeSopal).OnDelete(DeleteBehavior.NoAction)
  );
        }
    }
}