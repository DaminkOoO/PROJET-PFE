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
    public interface IVehiculeService
    {
        Task<ServiceResponse<List<VehiculeDTO>?>> GetAllAsync(QueryObject QueryObject);
        Task<ServiceResponse<VehiculeDTO>> GetVehiculeById(string matricule);
        Task<ServiceResponse<VehiculeDTO>> CreateVehicule(CreateVehiculeRequestDTO createVehiculeRequestDTO);
        Task<ServiceResponse<bool>> DeleteVehicule(string matricule);
        Task<ServiceResponse<VehiculeDTO>> UpdateVehicule(string matricule, UpdateVehiculeRequestDTO updateVehiculeRequestDTO);
        Task<ServiceResponse<List<VehiculeDTO>?>> GetAllVehiculesLibre();
        Task<ServiceResponse<bool>> SwitchLibre(string matricule);
        

    }
}