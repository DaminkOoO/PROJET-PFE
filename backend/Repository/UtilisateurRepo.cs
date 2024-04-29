using backend.Data;
using backend.Dtos.User;
using backend.Helpers;
using backend.Interfaces;
using backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace backend.Repository
{
    public class UtilisateurRepo : IUtilisateurRepo
    {
        private readonly UserManager<Utilisateur> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UtilisateurRepo(UserManager<Utilisateur> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IdentityResult> AddToRole(Utilisateur utilisateur, string role)
        {
            return await _userManager.AddToRoleAsync(utilisateur, role);
        }

        public async Task<IdentityResult> CreateAsync(Utilisateur utilisateur)
        {
            return await _userManager.CreateAsync(utilisateur);

        }

        public async Task<IdentityResult> DeleteAsync(Utilisateur utilisateurModel)
        {
            return await _userManager.DeleteAsync(utilisateurModel);
        }

        public async Task<List<Utilisateur>> GetAllAsync()
        {
            return await _userManager.Users.ToListAsync();

        }

        public async Task<Utilisateur?> GetByMatriculeAsync(string matricule)
        {
            var utilisateur = await _userManager.FindByIdAsync(matricule);

            return utilisateur;
        }

        public async Task<List<string?>> GetRoles()
        {
            var roles = await _roleManager.Roles.Select(r => r.Name).ToListAsync();
            return roles;
        }

        public async Task<IList<string>> GetUtilisateurRoles(Utilisateur utilisateurModel)
        {
            return await _userManager.GetRolesAsync(utilisateurModel);
        }

        public async Task<IdentityResult> RemoveFromRoleAsync(Utilisateur utilisateur, string role)
        {
            return await _userManager.RemoveFromRoleAsync(utilisateur, role);
        }

        public async Task<IdentityResult> UpdateAsync(Utilisateur UtilisateurModel)
        {
            try
            {

                var update = await _userManager.UpdateAsync(UtilisateurModel);

                return update;

            }
            catch (Exception ex)
            {
                Console.WriteLine("Repo" + ex.Message);
            }
            return new IdentityResult();

        }
    }
}
