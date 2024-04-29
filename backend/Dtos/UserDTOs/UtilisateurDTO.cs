using backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace backend.Dtos.User
{
    public class UtilisateurDTO
    {
        public string MatriculeSOPAL { get; set; } = string.Empty;
        public string Nom { get; set; } = string.Empty;
        public string Prenom { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
        public string MotDePasse { get; set; } = string.Empty;
        public string? Role { get; set; } = string.Empty;

    }
}