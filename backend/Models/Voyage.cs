using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models
{
    public class Voyage
    {
        [Key]
        public string IdVoyage { get; set; } = string.Empty;
        public DateTime DateSortieBon { get; set; }
        public DateTime DateDepartVoyage { get; set; }

        public string Etat { get; set; } = string.Empty;
        //Demandeur
        public Utilisateur Utilisateur { get; set; } = null!;
        public string IdDemandeur { get; set; } = string.Empty;
        public ICollection<Utilisateur> Utilisateurs { get; set; } = new List<Utilisateur>();
        public Utilisateur? Magasinier { get; set; }
        public string IdMagasinier { get; set; } = string.Empty;
        public TypeVoyage TypeVoyage { get; set; } = null!;
        // public int IdTypeVoyage { get; set; }
        // public List<UtilisateurVoyage> UtilisateurVoyage { get; } = [];


    }
}