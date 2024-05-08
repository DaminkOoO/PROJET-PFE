using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace backend.Models
{
    public class Transporteur
    {
        [Key]
        public string UserId { get; set; } = string.Empty;
        public Utilisateur Utilisateur { get; set; } = null!;
        public Vehicule? Vehicule { get; set; }
        public string? VehiculeMatricule { get; set; } = string.Empty;
        public bool ExterneDuService { get; set; } = false;
        public List<Voyage> Voyages { get; } = [];
        public ICollection<TransporteurVoyage> TransporteurVoyages { get; } = [];
        public Localisation? Localisation { get; set; } = null!;
        public int? LocalisationId { get; set; }
        public bool Active {get;set;} = true;
    }
}