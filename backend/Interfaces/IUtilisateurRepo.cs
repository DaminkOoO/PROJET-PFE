using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Dtos.User;
using backend.Helpers;
using backend.Models;
using Microsoft.AspNetCore.Identity;

namespace backend.Interfaces
{
    public interface IUtilisateurRepo
    {
        Task<List<Utilisateur>> GetAllAsync();
        Task<Utilisateur?> GetByMatriculeAsync(string matricule);
        Task<IdentityResult> AddToRole(Utilisateur utilisateur, string role);
        Task<IdentityResult> RemoveFromRole(Utilisateur utilisateur, string role);
        Task<IList<string>> GetUtilisateurRoles(Utilisateur utilisateurModel);
        Task<IdentityResult> CreateAsync(Utilisateur utilisateurModel);
        Task<IdentityResult> UpdateAsync(Utilisateur UtilisateurModel);
        Task<IdentityResult> DeleteAsync(Utilisateur utilisateurModel);
    }
}