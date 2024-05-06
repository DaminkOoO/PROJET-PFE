using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models
{
    public class Localisation
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        [Column(TypeName = "decimal(20, 17)")]
        public decimal Lattitude { get; set; } = 0;
        [Column(TypeName = "decimal(20, 17)")]
        public decimal Longitude { get; set; } = 0;
        public Transporteur Transporteur { get; set; } = new Transporteur();
        public List<TransporteurVoyage>? TransporteurVoyages { get; set; } = [];
    }
}