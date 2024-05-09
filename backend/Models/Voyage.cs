using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        public double Poids {get; set;}
        public int NbrePalette {get; set;}
    
        //Demandeur
        public Demandeur? Demandeur { get; set; } = null!;
        public string? DemandeurMatriculeSopal { get; set; } = string.Empty;
        [Column(TypeName = "decimal(20, 17)")]
        public decimal Priorite { get; set; }
        public Magasinier? MagasinierDemandeur { get; set; } = null!;
        public string? MagasinierDemandeurMatriculeSopal { get; set; } = string.Empty;
        public Magasinier Magasinier { get; set; } = null!;
        public string MagasinierMatriculeSopal { get; set; } = string.Empty;
        public TypeVoyage TypeVoyage { get; set; } = null!;
        public int TypeVoyageId { get; set; }
        public List<Transporteur> Transporteurs { get; } = [];
        public ICollection<TransporteurVoyage> TransporteurVoyages { get; } = [];
    }
}