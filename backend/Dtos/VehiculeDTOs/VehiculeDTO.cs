using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Dtos.VehiculeDTOs
{
    public class VehiculeDTO
    {

        public string Matricule { get; set; } = string.Empty;
        public string Marque { get; set; } = string.Empty;
        public string Modele { get; set; } = string.Empty;
        public double Poids { get; set; }
        public double Volume { get; set; }
        public bool Bloque { get; set; } = false;
        public string? TransporteurID { get; set; } = string.Empty;
        
}
    }