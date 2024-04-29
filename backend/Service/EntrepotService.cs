using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Dtos.EntrepotDTOs;
using backend.Helpers;
using backend.Interfaces;
using backend.Mappers;
using backend.Models;

namespace backend.Service
{
    public class EntrepotService : IEntrepotService
    {
        private readonly IEntrepotRepo _entrepotRepository;
        public EntrepotService(IEntrepotRepo entrepotRepository)
        {
            _entrepotRepository = entrepotRepository;
        }
        public async Task<EntrepotDTO> CreateEntrepot(RegisterEntrepotDTO registerEntrepotRequestDTO)
        {
            try
            {
                var entrepotModel = new Entrepot
                {
                    Id = registerEntrepotRequestDTO.Id,
                    Lattitude = registerEntrepotRequestDTO.Lattitude,
                    Longitude = registerEntrepotRequestDTO.Longitude,
                    Nom = registerEntrepotRequestDTO.Nom,
                };
                await _entrepotRepository.CreateAsync(entrepotModel);
                return entrepotModel.ToEntrepotDTO();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw new Exception(e.Message);
            }

        }

        public async Task<bool> DeleteEntrepot(string id)
        {
            try
            {
                var EntrepotModel = await _entrepotRepository.GetByIdAsync(id);
                if (EntrepotModel == null)
                    return false;
                var deletingUser = await _entrepotRepository.DeleteAsync(id);
                return deletingUser != null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw new Exception(e.Message);
            }
        }

        public async Task<List<EntrepotDTO>?> GetAllAsync(QueryObject QueryObject)
        {
            try
            {
                var entrepots = await _entrepotRepository.GetAllAsync(QueryObject);
                var entrepotsDTO = entrepots.Select(s => s.ToEntrepotDTO()).ToList();
                return entrepotsDTO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<EntrepotDTO?> GetByIdAsync(string id)
        {
            try
            {

                var Entrepot = await _entrepotRepository.GetByIdAsync(id);

                return Entrepot?.ToEntrepotDTO();
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }

        }

        public async Task<Entrepot?> UpdateEntrepot(UpdateEntrepotRequestDTO updateEntrepotRequestDTO, string id)
        {
            try
            {
                var EntrepotModel = await _entrepotRepository.UpdateAsync(id, updateEntrepotRequestDTO);
                return EntrepotModel;

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message, e.StackTrace);
                throw new Exception(e.Message);
            }
        }
    }

}