using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models
{
    public class Entrepot
    {
        [Key]
        public string Id { get; set; } = string.Empty;
        public string Nom { get; set; } = string.Empty;
        [Column(TypeName = "decimal(20, 17)")]
        public decimal Lattitude { get; set; }
        [Column(TypeName = "decimal(20, 17)")]
        public decimal Longitude { get; set; }
        public Magasinier Magasinier { get; set; } = null!;


    }
}