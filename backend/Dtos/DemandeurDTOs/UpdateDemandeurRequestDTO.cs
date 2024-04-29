using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using backend.Models;

namespace backend.Dtos.DemandeurDTO
{
    public class UpdateDemandeurRequestDTO
    {
        [Column(TypeName = "decimal(10, 10)")]
        public decimal Lattitude { get; set; }
        [Column(TypeName = "decimal(10, 10)")]
        public decimal Longitude { get; set; }
        public bool IsMagasinier { get; set; }
        public ICollection<Voyage> Voyages { get; set; } = [];
    }
}