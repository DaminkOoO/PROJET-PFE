using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Dtos.MagasinierDTOs;
using backend.Dtos.User;
using backend.Helpers;
using backend.Models;

namespace backend.Interfaces
{
    public interface IMagasinierRepo
    {
        Task<List<Magasinier>> GetAllAsync(QueryObject queryObject);
        Task<Magasinier?> GetByIdAsync(string matricule);
        Task<Magasinier> CreateAsync(Magasinier MagasinierModel);
        Task<Magasinier?> UpdateAsync(string matricule, UpdateUserRequestDTO MagasinierDto);
        Task<Magasinier?> DeleteAsync(string matricule);
    }
}