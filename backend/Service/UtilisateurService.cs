using backend.Dtos.User;
using backend.Interfaces;
using backend.Mappers;
using backend.Models;
using backend.Repository;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Service
{
    public class UtilisateurService : IUtilisateurService
    {
        private readonly IUtilisateurRepo _utilisateurRepo;
        public UtilisateurService(IUtilisateurRepo utilisateurRepo)
        {
            _utilisateurRepo = utilisateurRepo;
        }

        public async Task<IdentityResult> CreateUtilisateur(RegisterUserRequestDTO registerUserRequestDTO)
        {
            try
            {
                return await _utilisateurRepo.CreateAsync(new Utilisateur
                {
                    MatriculeSOPAL = registerUserRequestDTO.MatriculeSopal,
                    Email = registerUserRequestDTO.Email,
                    Nom = registerUserRequestDTO.Nom,
                    Prenom = registerUserRequestDTO.Prenom,
                    UserName = registerUserRequestDTO.MatriculeSopal
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new Exception(e.Message);
            }
        }
        public async Task<IdentityResult> SetUserRole(Utilisateur utilisateur, string role)
        {
            try
            {
                return await _utilisateurRepo.AddToRole(utilisateur, role);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new Exception(e.Message);
            }
        }
        public async Task<bool> DeleteUtilisateur(string matricule)
        {
            try
            {
                var utilisateurModel = await _utilisateurRepo.GetByMatriculeAsync(matricule);
                if (utilisateurModel == null)
                    return false;
                var deletingUser = await _utilisateurRepo.DeleteAsync(utilisateurModel);
                if (deletingUser == null) return false;
                return deletingUser.Succeeded;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        public async Task<List<Utilisateur>?> GetAllAsync()
        {

            try
            {
                return await _utilisateurRepo.GetAllAsync();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

            }
            return null;
        }

        public async Task<UtilisateurDTO?> GetByMatriculeAsync(string matricule)
        {
            var utilisateur = await _utilisateurRepo.GetByMatriculeAsync(matricule);
            if (utilisateur == null) return null;
            var userRoles = await _utilisateurRepo.GetUtilisateurRoles(utilisateur);
            var utilisateurDTO = utilisateur.ToUtilisateurDTO();
            utilisateurDTO.Role = userRoles.FirstOrDefault();

            return utilisateurDTO;
        }

        public async Task<Utilisateur?> UpdateUtilisateur(UpdateUserRequestDTO updateUserRequestDTO, string matricule)
        {

            if (updateUserRequestDTO == null) return null;
            var utilisateurModel = await _utilisateurRepo.GetByMatriculeAsync(matricule);
            if (utilisateurModel == null)
            {

                return null;

            }
            var roleActuel = _utilisateurRepo.GetUtilisateurRoles(utilisateurModel).Result.FirstOrDefault();
            if (roleActuel != updateUserRequestDTO.Role)
            {
                if (roleActuel == null)
                {
                    Console.WriteLine("le role actuel est null");
                    await SetUserRole(utilisateurModel, updateUserRequestDTO.Role);

                }
                else
                {
                    var result = await _utilisateurRepo.RemoveFromRole(utilisateurModel, roleActuel);
                    await SetUserRole(utilisateurModel, updateUserRequestDTO.Role);
                }
            }
            utilisateurModel.Nom = updateUserRequestDTO.Nom ?? utilisateurModel.Nom;
            utilisateurModel.Prenom = updateUserRequestDTO.Prenom ?? utilisateurModel.Prenom;
            utilisateurModel.Email = updateUserRequestDTO.Email ?? utilisateurModel.Email;
            if (updateUserRequestDTO.Role == "Magasinier" && updateUserRequestDTO.EntrepotId != null)
            {
                utilisateurModel.EntrepotId = updateUserRequestDTO.EntrepotId;
                if (utilisateurModel.VehiculeId != null)
                {
                    utilisateurModel.VehiculeId = null;
                }
            }
            else
            {
                if (updateUserRequestDTO.Role == "Transporteur" && updateUserRequestDTO.VehiculeId != null)
                {
                    utilisateurModel.VehiculeId = updateUserRequestDTO.VehiculeId;
                    if (utilisateurModel.EntrepotId != null)
                    {
                        utilisateurModel.EntrepotId = null;
                    }
                }
                else
                {
                    if (updateUserRequestDTO.Role == "Admin" || updateUserRequestDTO.Role == "Demandeur")
                    {
                        utilisateurModel.VehiculeId = null;
                        utilisateurModel.EntrepotId = null;
                    }
                    else
                    {
                        return null;
                    }


                }
            }
            var updatingUser = await _utilisateurRepo.UpdateAsync(utilisateurModel);
            if (updatingUser == null) return null;
            return utilisateurModel;
        }
    }
}