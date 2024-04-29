using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models
{
    public class TypeVoyage
    {
        [Key]
        public int IdTypeVoyage { get; set; }
        public string Libelle { get; set; } = string.Empty;
        public ICollection<Voyage> Voyages { get; set; } = [];
    }
}