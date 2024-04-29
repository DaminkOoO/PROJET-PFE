using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Dtos.DemandeurDTOs;
using backend.Dtos.User;
using backend.Helpers;
using backend.Models;
using backend.Response;

namespace backend.Interfaces
{
    public interface IDemandeurService
    {
        Task<ServiceResponse<List<DemandeurDTO>?>> GetAllAsync(QueryObject DemandeurQueryObject);
        Task<ServiceResponse<DemandeurDTO?>> GetByMatriculeAsync(UtilisateurDTO d);
        Task<ServiceResponse<DemandeurDTO>> CreateDemandeur(RegisterUserRequestDTO registerUserRequestDTO);
        Task<ServiceResponse<DemandeurDTO?>> UpdateDemandeur(UpdateUserRequestDTO updateUserRequestDTO, string matricule);
        Task<ServiceResponse<bool>> DeleteDemandeur(string matricule);

    }
}