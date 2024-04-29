using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Dtos.VehiculeDTOs
{
    public class UpdateVehiculeRequestDTO
    {
        [Required]
        public string Marque { get; set; } = string.Empty;
        [Required]
        public string Modele { get; set; } = string.Empty;
        [Required]
        public double Poids { get; set; }
        [Required]
        public double Volume { get; set; }
        [Required]
        public bool Bloque { get; set; }
    }
}