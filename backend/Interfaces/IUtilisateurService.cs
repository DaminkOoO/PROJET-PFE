using backend.Dtos.User;
using backend.Models;
using Microsoft.AspNetCore.Identity;

namespace backend.Interfaces
{
    public interface IUtilisateurService
    {
        Task<List<Utilisateur>?> GetAllAsync();
        Task<UtilisateurDTO?> GetByMatriculeAsync(string matricule);
        Task<IdentityResult> CreateUtilisateur(RegisterUserRequestDTO registerUserRequestDTO);
        Task<Utilisateur?> UpdateUtilisateur (UpdateUserRequestDTO updateUserRequestDTO,string matricule);
        Task<bool> DeleteUtilisateur (string matricule);
        Task<IdentityResult> SetUserRole(Utilisateur utilisateur, string role);

    }
}
