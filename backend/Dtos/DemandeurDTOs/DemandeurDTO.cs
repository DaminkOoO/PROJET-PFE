using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using backend.Dtos.User;

namespace backend.Dtos.DemandeurDTOs
{
    public class DemandeurDTO : UtilisateurDTO
    {

        [Column(TypeName = "decimal(20, 17)")]
        public decimal Lattitude { get; set; }
        [Column(TypeName = "decimal(20, 17)")]
        public decimal Longitude { get; set; }
    }
}