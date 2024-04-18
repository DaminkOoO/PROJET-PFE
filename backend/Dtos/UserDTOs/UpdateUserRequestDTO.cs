using backend.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Dtos.User
{
    public class UpdateUserRequestDTO
    {
        [Required]
        public string Nom { get; set; } = string.Empty;
        [Required]
        public string Prenom { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        public string? MotDePasse { get; set; } = string.Empty;
        [Required]
        public string Role { get; set; } = string.Empty;
        public string? VehiculeId { get; set; } = string.Empty ;
        public string? EntrepotId {  get; set; } = string.Empty ;

    }
}