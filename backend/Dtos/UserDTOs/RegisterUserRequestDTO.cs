using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Dtos.User
{
    public class RegisterUserRequestDTO
    {
        [Required]
        public string MatriculeSopal { get; set; } = string.Empty;
        [Required]
        public string Nom { get; set; } = string.Empty;
        [Required]
        public string Prenom { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        [Required]
        public string Role { get; set; } = string.Empty;
        [Column(TypeName = "decimal(10, 10)")]
        public decimal Lattitude { get; set; }
        [Column(TypeName = "decimal(10, 10)")]
        public decimal Longitude { get; set; }
        public bool ExterneDuService { get; set; } = false;
        public string VehiculeId { get; set; } = string.Empty;
        public string EntrepotId { get; set; } = string.Empty;

    }
}