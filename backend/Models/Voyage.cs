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
        public Demandeur Demandeur { get; set; } = null!;
        public string DemandeurMatriculeSopal { get; set; } = string.Empty;

        public Magasinier Magasinier { get; set; } = null!;
        public string MagasinierMatriculeSopal { get; set; } = string.Empty;
        public TypeVoyage TypeVoyage { get; set; } = null!;
        public int TypeVoyageId { get; set; }
        public List<Transporteur> Transporteurs { get; } = [];
        public Entrepot EntrepotDemandeur { get; set; } = new Entrepot();
        public string EntrepotDemandeurId { get; set; } = string.Empty;
        public Entrepot EntrepotExpedition { get; set; } = new Entrepot();
        public string EntrepotExpeditionId { get; set; } = string.Empty;
        //public ICollection<TransporteurVoyage> TransporteurVoyages { get; } = [];
    }
}