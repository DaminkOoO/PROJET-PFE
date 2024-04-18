using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Dtos.VehiculeDTOs;
using backend.Models;

namespace backend.Mappers
{
    public static class VehiculeMapper
    {
        public static VehiculeDTO ToVehiculeDTO(this Vehicule vehiculeModel)
        {
            return new VehiculeDTO
            {
                Marque = vehiculeModel.Marque,
                Bloque = vehiculeModel.Bloque,
                Matricule = vehiculeModel.Matricule,
                Modele = vehiculeModel.Modele,
                Poids = vehiculeModel.Poids,
                
            };
        }
        public static Vehicule ToVehiculeFromCreateDTO(this CreateVehiculeRequestDTO vehicule)
        {
            return new Vehicule
            {
                Marque = vehicule.Marque,
                Bloque = vehicule.Bloque,
                Matricule = vehicule.Matricule,
                Modele = vehicule.Modele,
                Poids = vehicule.Poids,
                Volume = vehicule.Volume,
            };
        }
    }
}