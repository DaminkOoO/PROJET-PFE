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
        private readonly ApplicationDBContext _context;
        public UtilisateurRepo(UserManager<Utilisateur> userManager, ApplicationDBContext context)
        {
            _userManager = userManager;
            _context = context;
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
            try{
            return await _userManager.Users.Include(v => v.Vehicule).Include(e => e.Entrepot).ToListAsync();
                
            }catch(Exception e){
                Console.WriteLine(e.Message);
                throw new Exception(e.Message);
            }
        }

        public async Task<Utilisateur?> GetByMatriculeAsync(string matricule)
        {
            var utilisateur = await _userManager.FindByNameAsync(matricule);
            if (utilisateur != null)
            {
                await _context.Entry(utilisateur)
                    .Reference(u => u.Vehicule)
                    .LoadAsync();

                await _context.Entry(utilisateur)
                    .Reference(u => u.Entrepot)
                    .LoadAsync();
            }
            return utilisateur;
        }

        public async Task<IList<string>> GetUtilisateurRoles(Utilisateur utilisateurModel)
        {
            return await _userManager.GetRolesAsync(utilisateurModel);
        }

        public async Task<IdentityResult> RemoveFromRole(Utilisateur utilisateur, string role)
        {
            return await _userManager.RemoveFromRoleAsync(utilisateur, role);
        }

        public async Task<IdentityResult> UpdateAsync(Utilisateur UtilisateurModel)
        {
            try
            {
                Console.WriteLine("Hello");
                var update = await _userManager.UpdateAsync(UtilisateurModel);
                Console.WriteLine(update);
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
