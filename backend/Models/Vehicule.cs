using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models
{
    public class Vehicule
    {
        [Key]
        public string Matricule { get; set; } = string.Empty;
        public string Marque { get; set; } = string.Empty;
        public string Modele { get; set; } = string.Empty;
        public double PoidsActuel { get; set; }
        public double VolumeActuel { get; set; }
        public double Poids { get; set; }
        public double Volume { get; set; }
        public bool Bloque { get; set; }
        public bool IsLibre { get; set; } = true;
        public Transporteur? Transporteur;
    }
}