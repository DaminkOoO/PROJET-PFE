using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using backend.Dtos.User;

namespace backend.Dtos.DemandeurDTOs
{
    public class DemandeurDTO:UtilisateurDTO
    {

        [Column(TypeName = "decimal(10, 10)")]
        public decimal Lattitude { get; set; }
        [Column(TypeName = "decimal(10, 10)")]
        public decimal Longitude { get; set; }
    }
}