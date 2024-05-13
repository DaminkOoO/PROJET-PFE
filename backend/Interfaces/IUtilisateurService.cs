using backend.Dtos.User;
using backend.Dtos.UserDTOs;
using backend.Models;
using backend.Response;
using Microsoft.AspNetCore.Identity;

namespace backend.Interfaces
{
    public interface IUtilisateurService
    {
        //
        Task<ServiceResponse<List<UtilisateurDTO>?>> GetAllAsync();
        Task<ServiceResponse<UtilisateurDTO?>> GetByMatriculeAsync(string matricule);
        Task<ServiceResponse<LoginUserResponseDTO>> Login(LoginUserRequestDTO LoginUserRequestDTO);
        Task<ServiceResponse<IdentityResult>> CreateUtilisateur(RegisterUserRequestDTO registerUserRequestDTO);
        Task<ServiceResponse<Utilisateur?>> UpdateUtilisateur(UpdateUserRequestDTO updateUserRequestDTO, string matricule);
        Task<ServiceResponse<bool>> DeleteUtilisateur(string matricule);
        Task<ServiceResponse<IdentityResult>> SetUserRole(Utilisateur utilisateur, string role);
        Task<ServiceResponse<List<string>>> GetRoles();
        Task<ServiceResponse<bool>> IsExistRole(Utilisateur utilisateur, string role);
        Task<ServiceResponse<bool>> DeleteAdmin(string matricule);
        Task<ServiceResponse<Utilisateur?>> UpdateToken(string matricule, string token);
    }


}
