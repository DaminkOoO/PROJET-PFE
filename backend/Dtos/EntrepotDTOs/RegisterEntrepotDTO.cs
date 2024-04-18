using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Dtos.EntrepotDTOs
{
    public class RegisterEntrepotDTO
    {
        [Required]
        public string Id { get; set; } = string.Empty;
        [Required]
        public string Nom { get; set; } = string.Empty;
        [Required]
        public decimal Lattitude { get; set; }
        [Required]
        public decimal Longitude { get; set; }
    }
}