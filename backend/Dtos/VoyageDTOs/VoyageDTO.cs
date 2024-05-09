using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Dtos.VoyageDTOs
{
    public class VoyageDTO
    {
        public string IdVoyage { get; set; } = string.Empty;
        public DateTime DateSortieBon { get; set; }
        public DateTime DateDepartVoyage { get; set; }
        public string Etat { get; set; } = string.Empty;
        public string DemandeurMatriculeSopal { get; set; } = string.Empty;
        [Column(TypeName = "decimal(20, 17)")]
        public decimal Priorite { get; set; }
        public string MagasinierMatriculeSopal { get; set; } = string.Empty;
        public string MagasinierDemandeurMatriculeSopal { get; set; } = string.Empty;
        public double Poids { get; set; }
        public int NbrePalette { get; set; }
        public string TypeVoyage { get; set; } = string.Empty;
        public string TransporteursId { get; set; } = string.Empty;
    }
}