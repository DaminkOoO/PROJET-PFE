using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Dtos.LocalisationDTO;
using backend.Dtos.TransporteurDTOs;
using backend.Dtos.User;
using backend.Helpers;
using backend.Models;
using backend.Response;

namespace backend.Interfaces
{
    public interface ITransporteurService
    {
        Task<ServiceResponse<List<TransporteurDTO>?>> GetAllAsync(QueryObject TransporteurQueryObject);
        Task<ServiceResponse<TransporteurDTO?>> GetByMatriculeAsync(UtilisateurDTO u);
        Task<ServiceResponse<TransporteurDTO>> CreateTransporteur(RegisterUserRequestDTO registerUserRequestDTO);
        Task<ServiceResponse<TransporteurDTO?>> UpdateTransporteur(UpdateUserRequestDTO updateUserRequestDTO, string matricule);
        Task<ServiceResponse<bool>> DeleteTransporteur(string matricule);
        Task<ServiceResponse<LocalisationDTO>> UpdatePostion(string matricule, LocalisationDTO localisationDTO);
        Task<ServiceResponse<Transporteur?>> UpdateToken(string matricule, string token);
    }
}