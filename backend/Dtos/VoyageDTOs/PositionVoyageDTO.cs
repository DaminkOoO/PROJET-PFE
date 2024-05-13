using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using backend.Dtos.LocalisationDTOs;
using backend.Models;

namespace backend.Dtos.VoyageDTOs
{
    public class PositionVoyageDTO
    {
        public LocalisationDTO LocalisationDebut { get; set; } = new LocalisationDTO();
        public LocalisationDTO LocalisationFin { get; set; } = new LocalisationDTO();
    }
}