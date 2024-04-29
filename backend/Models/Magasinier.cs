using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace backend.Models
{
    public class Magasinier
    {
        [Key]
        public string MatriculeSopal { get; set; } = string.Empty;
        public Utilisateur Utilisateur { get; set; } = null!;
        public Entrepot Entrepot { get; set; } = null!;
        public string EntrepotId { get; set; } = string.Empty;
        public ICollection<Voyage> Voyages { get; set; } = [];
    }
}