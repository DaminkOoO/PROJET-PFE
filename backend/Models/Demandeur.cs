using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models
{
    public class Demandeur : Utilisateur
    {
        public decimal Lattitude { get; set; }
        public decimal Longitude { get; set; }
    }
}