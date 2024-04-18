using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Dtos.EntrepotDTOs
{
    public class UpdateEntrepotRequestDTO
    {
        [Required]
        public string Nom { get; set; } = string.Empty;
        [Required]
        public decimal Lattitude { get; set; }
        [Required]
        public decimal Longitude { get; set; }
    }
}