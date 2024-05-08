using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Dtos.VoyageDTOs;
using backend.Models;

namespace backend.Mappers
{
    public static class VoyageMapper
    {
        public static VoyageDTO ToVoyageDTO(this Voyage voyage)
        {
            var transporteur = voyage.Transporteurs[voyage.Transporteurs.Count - 1].UserId;
            return new VoyageDTO
            {
                DateDepartVoyage = voyage.DateDepartVoyage,
                DateSortieBon = voyage.DateSortieBon,
                DemandeurMatriculeSopal = voyage.DemandeurMatriculeSopal,
                Etat = voyage.Etat,
                IdVoyage = voyage.IdVoyage,
                MagasinierMatriculeSopal = voyage.MagasinierMatriculeSopal,
                Priorite = voyage.Priorite,
                TransporteursId = transporteur,
                TypeVoyage = voyage.TypeVoyage.Libelle,
            };
        }
        public static Voyage ToVoyage(this VoyageDTO voyageDTO)
        {
            return new Voyage
            {
                DateDepartVoyage = voyageDTO.DateDepartVoyage,
                DateSortieBon = voyageDTO.DateSortieBon,
                DemandeurMatriculeSopal = voyageDTO.DemandeurMatriculeSopal,
                Etat = voyageDTO.Etat,
                IdVoyage = voyageDTO.IdVoyage,
                MagasinierMatriculeSopal = voyageDTO.MagasinierMatriculeSopal,
                Priorite = voyageDTO.Priorite,
            };
        }
        
    }
}