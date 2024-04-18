using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Dtos.EntrepotDTOs;
using backend.Models;

namespace backend.Mappers
{
    public static class EntrepotMapper
    {
        public static EntrepotDTO ToEntrepotDTO(this Entrepot entrepot)
        {
            return new EntrepotDTO{
                Id = entrepot.Id,
                Lattitude = entrepot.Lattitude,
                Longitude = entrepot.Longitude,
                Nom = entrepot.Nom,
            };
        }
    }
}