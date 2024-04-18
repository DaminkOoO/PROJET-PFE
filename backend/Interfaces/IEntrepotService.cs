using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Dtos.EntrepotDTOs;
using backend.Helpers;
using backend.Models;

namespace backend.Interfaces
{
    public interface IEntrepotService
    {
        Task<List<EntrepotDTO>?> GetAllAsync(QueryObject QueryObject);
        Task<EntrepotDTO?> GetByIdAsync(string id);
        Task<EntrepotDTO> CreateEntrepot(RegisterEntrepotDTO registerEntrepotRequestDTO);
        Task<Entrepot?> UpdateEntrepot(UpdateEntrepotRequestDTO updateEntrepotRequestDTO, string id);
        Task<bool> DeleteEntrepot(string matricule);
    }
}