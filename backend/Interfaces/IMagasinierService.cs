using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Dtos.MagasinierDTOs;
using backend.Dtos.User;
using backend.Helpers;
using backend.Response;

namespace backend.Interfaces
{
    public interface IMagasinierService
    {

        Task<ServiceResponse<List<MagasinierDTO>?>> GetAllAsync(QueryObject MagasinierQueryObject);
        Task<ServiceResponse<MagasinierDTO?>> GetByMatriculeAsync(UtilisateurDTO u);
        Task<ServiceResponse<MagasinierDTO>> CreateMagasinier(RegisterUserRequestDTO registerUserRequestDTO);
        Task<ServiceResponse<MagasinierDTO?>> UpdateMagasinier(UpdateUserRequestDTO updateUserRequestDTO, string matricule);
        Task<ServiceResponse<bool>> DeleteMagasinier(string matricule);
    }
}