
using backend.Dtos.EntrepotDTOs;
using backend.Helpers;
using backend.Models;

namespace backend.Interfaces
{
    public interface IEntrepotRepo
    {
        Task<List<Entrepot>> GetAllAsync(QueryObject QueryObject);
        Task<Entrepot?> GetByIdAsync(string id);
        Task<Entrepot> CreateAsync(Entrepot VehiculeModel);
        Task<Entrepot?> UpdateAsync(string id, UpdateEntrepotRequestDTO EntrepotDto);
        Task<Entrepot?> DeleteAsync(string id);
    }
}