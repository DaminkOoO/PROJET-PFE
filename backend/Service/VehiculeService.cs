using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Dtos.VehiculeDTOs;
using backend.Helpers;
using backend.Interfaces;
using backend.Mappers;
using backend.Response;

namespace backend.Service
{
    public class VehiculeService : IVehiculeService
    {
        public readonly IVehiculeRepo _vehiculeRepo;
        public VehiculeService(IVehiculeRepo vehiculeRepo)
        {
            _vehiculeRepo = vehiculeRepo;
        }

        public async Task<ServiceResponse<VehiculeDTO>> CreateVehicule(CreateVehiculeRequestDTO createVehicule)
        {
            var serviceResponse = new ServiceResponse<VehiculeDTO>();
            try
            {
                var vehiculeModel = createVehicule.ToVehiculeFromCreateDTO();
                if (await _vehiculeRepo.GetByIdAsync(vehiculeModel.Matricule) == null)
                {
                    await _vehiculeRepo.CreateAsync(vehiculeModel);
                    serviceResponse.Success = true;
                    serviceResponse.Message = "Véhicule ajouté";
                    serviceResponse.Data = vehiculeModel.ToVehiculeDTO();
                    return serviceResponse;
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Véhicule déjà existant";
                    return serviceResponse;
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = $"Une erreur s'est produite : {ex.Message}";
                return serviceResponse;
            }
        }

        public async Task<ServiceResponse<bool>> DeleteVehicule(string matricule)
        {
            var serviceResponse = new ServiceResponse<bool>();
            try
            {
                var deletedVehicule = await _vehiculeRepo.DeleteAsync(matricule);
                if (deletedVehicule == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Véhicule non trouvé";
                    return serviceResponse;
                }
                else
                {
                    serviceResponse.Success = true;
                    serviceResponse.Message = "Véhicule supprimé";
                    return serviceResponse;
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = $"Une erreur s'est produite : {ex.Message}";
                return serviceResponse;
            }
        }

        public async Task<ServiceResponse<List<VehiculeDTO>?>> GetAllAsync(QueryObject queryObject)
        {
            var serviceResponse = new ServiceResponse<List<VehiculeDTO>?>();
            try
            {
                var vehicules = await _vehiculeRepo.GetAllAsync(queryObject);
                var vehiculeDTOs = vehicules.Select(v => v.ToVehiculeDTO()).ToList();
                serviceResponse.Success = true;
                serviceResponse.Message = "Liste des véhicules récupérée";
                serviceResponse.Data = vehiculeDTOs;
                return serviceResponse;
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = $"Une erreur s'est produite : {ex.Message}";
                return serviceResponse;
            }
        }

        public async Task<ServiceResponse<List<VehiculeDTO>?>> GetAllVehiculesLibre()
        {
            var serviceResponse = new ServiceResponse<List<VehiculeDTO>?>();
            try
            {

                var vehicules = await _vehiculeRepo.GetAllAsync(new QueryObject());
                var vehiculesLibre = vehicules.Where(v => v.IsLibre && !v.Bloque);
                var vehiculeDTOs = vehiculesLibre.Select(v => v.ToVehiculeDTO()).ToList();
                serviceResponse.Success = true;
                serviceResponse.Message = "Liste des véhicules libres récupérée";
                serviceResponse.Data = vehiculeDTOs;
                return serviceResponse;
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = $"Une erreur s'est produite : {ex.Message}";
                return serviceResponse;
            }
        }

        public async Task<ServiceResponse<VehiculeDTO>> GetVehiculeById(string matricule)
        {
            var serviceResponse = new ServiceResponse<VehiculeDTO>();
            try
            {
                var vehicule = await _vehiculeRepo.GetByIdAsync(matricule);
                if (vehicule == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Véhicule non trouvé";
                    return serviceResponse;
                }
                else
                {
                    serviceResponse.Success = true;
                    serviceResponse.Message = "Véhicule récupéré";
                    serviceResponse.Data = vehicule.ToVehiculeDTO();
                    return serviceResponse;
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = $"Une erreur s'est produite : {ex.Message}";
                return serviceResponse;
            }
        }

        public async Task<ServiceResponse<bool>> SwitchLibre(string matricule)
        {
            Console.WriteLine("Switch Libre" + matricule);
            var serviceResponse = new ServiceResponse<bool>();
            try
            {
                var etat = await _vehiculeRepo.SwitchLibre(matricule);
                if (etat == true)
                {
                    serviceResponse.Success = true;
                    serviceResponse.Message = "";
                    return serviceResponse;
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Erreur!";
                    return serviceResponse;
                }

            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = $"Une erreur s'est produite : {ex.Message}";
                return serviceResponse;
            }

        }

        public async Task<ServiceResponse<VehiculeDTO>> UpdateVehicule(string matricule, UpdateVehiculeRequestDTO updateVehiculeRequestDTO)
        {
            var serviceResponse = new ServiceResponse<VehiculeDTO>();
            try
            {
                var vehiculeModel = await _vehiculeRepo.UpdateAsync(matricule, updateVehiculeRequestDTO);
                if (vehiculeModel == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Véhicule non trouvé";
                    return serviceResponse;
                }
                else
                {
                    serviceResponse.Success = true;
                    serviceResponse.Message = "Véhicule mis à jour";
                    serviceResponse.Data = vehiculeModel.ToVehiculeDTO();
                    return serviceResponse;
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = $"Une erreur s'est produite : {ex.Message}";
                return serviceResponse;
            }
        }
    }
}
