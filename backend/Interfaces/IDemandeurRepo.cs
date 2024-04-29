using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Dtos.DemandeurDTO;
using backend.Dtos.User;
using backend.Helpers;
using backend.Models;

namespace backend.Interfaces
{
    public interface IDemandeurRepo
    {
        Task<List<Demandeur>> GetAllAsync(QueryObject DemandeurQueryObject);
        Task<Demandeur?> GetByIdAsync(string matricule);
        Task<Demandeur> CreateAsync(Demandeur DemandeurModel);
        Task<Demandeur?> UpdateAsync(string matricule, UpdateUserRequestDTO DemandeurDto);
        Task<Demandeur?> DeleteAsync(string matricule);

    }
}