using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Dtos.EntrepotDTOs
{
    public class RegisterEntrepotDTO
    {

        public string Id { get; set; } = string.Empty;
        [Required]
        public string Nom { get; set; } = string.Empty;
        [Required]
        [Column(TypeName = "decimal(20, 17)")]
        public decimal Lattitude { get; set; }
        [Required]
        [Column(TypeName = "decimal(20, 17)")]
        public decimal Longitude { get; set; }
    }
}