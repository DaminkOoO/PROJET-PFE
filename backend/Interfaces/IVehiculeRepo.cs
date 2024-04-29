using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Dtos.VehiculeDTOs;
using backend.Helpers;
using backend.Models;
using backend.Response;

namespace backend.Interfaces
{
    public interface IVehiculeRepo
    {
        Task<List<Vehicule>> GetAllAsync(QueryObject vehiculeQueryObject);
        Task<Vehicule?> GetByIdAsync(string matricule);
        Task<Vehicule> CreateAsync(Vehicule VehiculeModel);
        Task<Vehicule?> UpdateAsync(string matricule, UpdateVehiculeRequestDTO VehiculeDto);
        Task<Vehicule?> DeleteAsync(string matricule);
        Task<bool> SwitchLibre(string matricule);
    }
}