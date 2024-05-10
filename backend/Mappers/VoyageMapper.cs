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

            return new VoyageDTO
            {
                MagasinierDemandeurMatriculeSopal = voyage.MagasinierDemandeurMatriculeSopal,
                DateSortieBon = voyage.DateSortieBon,
                Etat = voyage.Etat,
                IdVoyage = voyage.IdVoyage,
                MagasinierMatriculeSopal = voyage.MagasinierMatriculeSopal,
                Priorite = voyage.Priorite,
                TransporteursId = voyage.TransporteurId,
                TypeVoyage = voyage.TypeVoyage.Libelle,
                NbrePalette = voyage.NbrePalette,
                Poids = voyage.Poids
            };
        }
        public static Voyage ToVoyage(this VoyageDTO voyageDTO)
        {
            return new Voyage
            {
                DateDepartVoyage = voyageDTO.DateDepartVoyage,
                DateSortieBon = voyageDTO.DateSortieBon,
                DemandeurMatriculeSopal = voyageDTO.DemandeurMatriculeSopal,
                MagasinierDemandeurMatriculeSopal = voyageDTO.MagasinierDemandeurMatriculeSopal,
                Etat = voyageDTO.Etat,
                IdVoyage = voyageDTO.IdVoyage,
                MagasinierMatriculeSopal = voyageDTO.MagasinierMatriculeSopal,
                Priorite = voyageDTO.Priorite,
                NbrePalette = voyageDTO.NbrePalette,
                Poids = voyageDTO.Poids
            };
        }

    }
}