using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Dtos.EntrepotDTOs
{
    public class EntrepotDTO
    {
        public string Id { get; set; } = string.Empty;
        public string Nom { get; set; } = string.Empty;
        public decimal Lattitude { get; set; }
        public decimal Longitude { get; set; }
    }
}