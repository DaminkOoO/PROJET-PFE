using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Dtos.EntrepotDTOs;
using backend.Helpers;
using backend.Interfaces;
using backend.Mappers;
using backend.Models;
using backend.Response;

namespace backend.Service
{
    public class EntrepotService : IEntrepotService
    {
        private readonly IEntrepotRepo _entrepotRepository;
        public EntrepotService(IEntrepotRepo entrepotRepository)
        {
            _entrepotRepository = entrepotRepository;
        }

        public async Task<ServiceResponse<EntrepotDTO>> CreateEntrepot(RegisterEntrepotDTO registerEntrepotRequestDTO)
        {
            var serviceResponse = new ServiceResponse<EntrepotDTO>();

            try
            {
                var entrepot = new Entrepot
                {
                    Id = registerEntrepotRequestDTO.Id,
                    Nom = registerEntrepotRequestDTO.Nom,
                    Lattitude = registerEntrepotRequestDTO.Lattitude,
                    Longitude = registerEntrepotRequestDTO.Longitude,
                };
                Console.WriteLine("$$$$$$$$$$$$$$$$$$",entrepot.Lattitude);
                var newEntrepot = await _entrepotRepository.CreateAsync(entrepot);
                serviceResponse.Success = true;
                serviceResponse.Message = "Entrepot créé avec succès";
                serviceResponse.Data = newEntrepot.ToEntrepotDTO();
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = $"Une erreur s'est produite : {ex.Message}";
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<bool>> DeleteEntrepot(string matricule)
        {
            var serviceResponse = new ServiceResponse<bool>();

            try
            {
                var isDeleted = await _entrepotRepository.DeleteAsync(matricule);
                if (isDeleted != null)
                {
                    serviceResponse.Success = true;
                    serviceResponse.Message = "Entrepot supprimé avec succès!";
                    serviceResponse.Data = true;
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Erreur! Entrepot non supprimé!";
                    serviceResponse.Data = false;
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = $"Une erreur s'est produite : {ex.Message}";
            }

            return serviceResponse;
        }


        public async Task<ServiceResponse<List<EntrepotDTO>?>> GetAllAsync(QueryObject QueryObject)
        {
            var serviceResponse = new ServiceResponse<List<EntrepotDTO>?>();

            try
            {
                var entrepots = await _entrepotRepository.GetAllAsync(QueryObject);
                var entrepotsDTO = entrepots.Select(e => e.ToEntrepotDTO()).ToList();
                serviceResponse.Success = true;
                serviceResponse.Message = "Liste des entrepôts récupérée avec succès";
                serviceResponse.Data = entrepotsDTO;
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = $"Une erreur s'est produite : {ex.Message}";
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<EntrepotDTO?>> GetByIdAsync(string id)
        {
            var serviceResponse = new ServiceResponse<EntrepotDTO?>();

            try
            {
                var entrepot = await _entrepotRepository.GetByIdAsync(id);
                if (entrepot != null)
                {
                    serviceResponse.Success = true;
                    serviceResponse.Message = "Entrepot trouvé";
                    serviceResponse.Data = entrepot.ToEntrepotDTO();
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Entrepot introuvable";
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = $"Une erreur s'est produite : {ex.Message}";
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<EntrepotDTO?>> UpdateEntrepot(UpdateEntrepotRequestDTO updateEntrepotRequestDTO, string id)
        {
            var serviceResponse = new ServiceResponse<EntrepotDTO?>();

            try
            {
                var updatedEntrepot = await _entrepotRepository.UpdateAsync(id, updateEntrepotRequestDTO);
                if (updatedEntrepot != null)
                {
                    serviceResponse.Success = true;
                    serviceResponse.Message = "Entrepot mis à jour avec succès";
                    serviceResponse.Data = updatedEntrepot.ToEntrepotDTO();
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Entrepot introuvable";
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = $"Une erreur s'est produite : {ex.Message}";
            }

            return serviceResponse;
        }
    }
}