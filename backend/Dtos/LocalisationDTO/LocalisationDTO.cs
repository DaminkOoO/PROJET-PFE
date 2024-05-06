using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Dtos.LocalisationDTO
{
    public class LocalisationDTO
    {

        [Column(TypeName = "decimal(20, 17)")]
        public decimal Lattitude { get; set; }
        [Column(TypeName = "decimal(20, 17)")]
        public decimal Longitude { get; set; }
    }
}