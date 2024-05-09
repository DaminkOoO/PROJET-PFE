using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Dtos.TransporteurDTOs;
using backend.Dtos.User;
using backend.Helpers;
using backend.Models;

namespace backend.Interfaces
{
    public interface ITransporteurRepo
    {
        Task<List<Transporteur>> GetAllAsync(QueryObject QueryObject);
        Task<List<Transporteur>> GetTransporteursDisponible();
        Task<Transporteur?> GetByIdAsync(string matricule);
        Task<Transporteur> CreateAsync(Transporteur TransporteurModel);
        Task<Transporteur?> UpdateAsync(string matricule, UpdateUserRequestDTO TransporteurDto);
        Task<Transporteur?> DeleteAsync(string matricule);
        Task<Transporteur?> UpdatePostion(string matricule, Localisation localisation);
    }
}