using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Models;

namespace backend.Dtos.VoyageDTOs
{
    public class PositionVoyageDTO
    {
        public Localisation LocalisationDebut { get; set; } = new Localisation();
        public Localisation LocalisationFin { get; set; } = new Localisation();
    }
}