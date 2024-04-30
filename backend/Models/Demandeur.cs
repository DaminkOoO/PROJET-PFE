using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace backend.Models
{
    public class Demandeur
    {
        [Key]
        public string UserId { get; set; } = string.Empty;
        public Utilisateur Utilisateur { get; set; } = null!;
        [Column(TypeName = "decimal(20, 17)")]
        public decimal Lattitude { get; set; }
        [Column(TypeName = "decimal(20, 17)")]
        public decimal Longitude { get; set; }
        public bool IsMagasinier { get; set; }
        public ICollection<Voyage> Voyages { get; set; } = [];
    }
}