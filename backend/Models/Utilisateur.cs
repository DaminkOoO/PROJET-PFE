using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models
{
    public class Utilisateur : IdentityUser
    {
        [Key]
        public string MatriculeSOPAL { get; set; } = string.Empty;
        public string Nom { get; set; } = string.Empty;
        public string Prenom { get; set; } = string.Empty;
        public Entrepot? Entrepot { get; set; }
        public string? EntrepotId { get; set; } = string.Empty;
        public Vehicule? Vehicule { get; set; }
        public string? VehiculeId { get; set; } = string.Empty;
        // public Voyage? Voyage { get; set; }
        // public List<UtilisateurVoyage> UtilisateurVoyage { get; } = [];
        // public ICollection<Voyage> Voyages { get; set; } = new List<Voyage>();
    }
}