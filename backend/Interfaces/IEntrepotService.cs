using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Dtos.EntrepotDTOs;
using backend.Helpers;
using backend.Models;
using backend.Response;

namespace backend.Interfaces
{
    public interface IEntrepotService
    {
        Task<ServiceResponse<List<EntrepotDTO>?>> GetAllAsync(QueryObject QueryObject);
        Task<ServiceResponse<EntrepotDTO?>> GetByIdAsync(string id);
        Task<ServiceResponse<EntrepotDTO>> CreateEntrepot(RegisterEntrepotDTO registerEntrepotRequestDTO);
        Task<ServiceResponse<EntrepotDTO?>> UpdateEntrepot(UpdateEntrepotRequestDTO updateEntrepotRequestDTO, string id);
        Task<ServiceResponse<bool>> DeleteEntrepot(string matricule);
    }
}