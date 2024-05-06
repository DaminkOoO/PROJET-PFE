using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using backend.Dtos.MagasinierDTOs;
using backend.Dtos.User;
using backend.Helpers;
using backend.Interfaces;
using backend.Mappers;
using backend.Models;
using backend.Response;

namespace backend.Service
{
    public class MagasinierService : IMagasinierService
    {
        private readonly IMagasinierRepo _magasinierRepository;
        private readonly IUtilisateurService _utilisateurService;

        public MagasinierService(IMagasinierRepo magasinierRepository, IUtilisateurService utilisateurService)
        {
            _magasinierRepository = magasinierRepository;
            _utilisateurService = utilisateurService;
        }
        public async Task<ServiceResponse<MagasinierDTO>> CreateMagasinier(RegisterUserRequestDTO registerUserRequestDTO)
        {
            var serviceResponse = new ServiceResponse<MagasinierDTO>();
            // if (registerUserRequestDTO == null)
            // {
            //     serviceResponse.Success = false;
            //     serviceResponse.Message = "Veuillez remplir tous les champs";
            //     return serviceResponse;
            // }
            try
            {
                var utilisateur = await _utilisateurService.GetByMatriculeAsync(registerUserRequestDTO.MatriculeSopal);
                if (utilisateur.Success)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Utilisateur déja existant!";
                    return serviceResponse;
                }
                var user = await _utilisateurService.CreateUtilisateur(registerUserRequestDTO);
                if (user.Success)
                {
                    await _magasinierRepository.CreateAsync(new Magasinier
                    {
                        MatriculeSopal = registerUserRequestDTO.MatriculeSopal,
                        EntrepotId = registerUserRequestDTO.EntrepotId,

                    });
                    serviceResponse.Data = new MagasinierDTO
                    {
                        Email = registerUserRequestDTO.Email,
                        EntrepotId = registerUserRequestDTO.EntrepotId,
                        MatriculeSOPAL = registerUserRequestDTO.MatriculeSopal,
                        Nom = registerUserRequestDTO.Nom,
                        Prenom = registerUserRequestDTO.Prenom,
                        Role = "Magasinier"

                    };
                    serviceResponse.Success = true;
                    serviceResponse.Message = "Magasinier créé avec succès";
                    return serviceResponse;
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Erreur! Vérifier les champs";
                    return serviceResponse;
                }

            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = $"Une erreur s'est produite : {ex.Message}";
                return serviceResponse;
            }
        }

        public async Task<ServiceResponse<bool>> DeleteMagasinier(string matricule)
        {
            var serviceResponse = new ServiceResponse<bool>();
            try
            {
                var utilisateur = await _utilisateurService.GetByMatriculeAsync(matricule);
                if (!utilisateur.Success)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Erreur! Utilisateur non existant!";
                    return serviceResponse;
                }
                var doesExist = await _utilisateurService.IsExistRole(utilisateur.Data!.ToUtilisateur(), "Magasinier");
                if (!doesExist.Success)
                {
                    if (doesExist.Success == false)
                    {

                        return doesExist;
                    }
                }
                var deleteUtilisateur = await _utilisateurService.DeleteUtilisateur(matricule);
                if (deleteUtilisateur.Success)
                {
                    await _magasinierRepository.DeleteAsync(matricule);
                    serviceResponse.Data = true;
                    serviceResponse.Message = "Demandeur Supprimé avec succée!!";
                    return serviceResponse;
                }
                serviceResponse.Data = false;
                serviceResponse.Message = "Erreur!";
                return serviceResponse;
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = $"Erreur s'est produite : {ex.Message}";
                return serviceResponse;
            }
        }

        public async Task<ServiceResponse<List<MagasinierDTO>?>> GetAllAsync(QueryObject magasinierQueryObject)
        {
            var serviceResponse = new ServiceResponse<List<MagasinierDTO>?>();

            try
            {
                var utilisateurs = await _utilisateurService.GetAllAsync();
                var magasiniers = await _magasinierRepository.GetAllAsync(magasinierQueryObject);
                var query = from t in magasiniers
                            join u in utilisateurs.Data!
                            on t.MatriculeSopal equals u.MatriculeSOPAL
                            select new MagasinierDTO
                            {
                                Email = u.Email,
                                MatriculeSOPAL = t.MatriculeSopal,
                                Nom = u.Nom,
                                Prenom = u.Prenom,
                                EntrepotId = t.EntrepotId,
                                Role = "Magasinier",

                            };
                serviceResponse.Success = true;
                serviceResponse.Message = "";
                serviceResponse.Data = query.ToList();
                return serviceResponse;
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = $"Erreur s'est produite : {ex.Message}";
                return serviceResponse;
            }
        }

        public async Task<ServiceResponse<MagasinierDTO?>> GetByMatriculeAsync(UtilisateurDTO u)
        {
            var serviceResponse = new ServiceResponse<MagasinierDTO?>();
            try
            {


                var magasinier = await _magasinierRepository.GetByIdAsync(u.MatriculeSOPAL);

                if (u == null || magasinier == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Erreur! Utilisateur non existant!";
                    return serviceResponse;
                }
                serviceResponse.Success = true;
                serviceResponse.Data = new MagasinierDTO
                {
                    Email = u.Email,
                    MatriculeSOPAL = magasinier!.MatriculeSopal,
                    Nom = u.Nom,
                    Prenom = u.Prenom,
                    EntrepotId = magasinier.EntrepotId,
                    Role = "Magasinier",
                };
                return serviceResponse;
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = $"Erreur s'est produite : {ex.Message}";
                return serviceResponse;
            }
        }

        public async Task<ServiceResponse<MagasinierDTO?>> UpdateMagasinier(UpdateUserRequestDTO updateUserRequestDTO, string matricule)
        {
            var serviceResponse = new ServiceResponse<MagasinierDTO?>();
            try
            {
                var u = await _utilisateurService.GetByMatriculeAsync(matricule);
                var d = await _magasinierRepository.GetByIdAsync(matricule);
                if (!u.Success || d == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Erreur! Utilisateur non existant!";
                    return serviceResponse;
                }
                var utilisateur = await _utilisateurService.UpdateUtilisateur(updateUserRequestDTO, matricule);
                if (utilisateur.Success)
                {
                    var demandeur = await _magasinierRepository.UpdateAsync(matricule, updateUserRequestDTO);
                    if (demandeur != null)
                    {
                        serviceResponse.Success = true;
                        serviceResponse.Message = "";
                        serviceResponse.Data = new MagasinierDTO
                        {
                            Nom = updateUserRequestDTO.Nom,
                            Prenom = updateUserRequestDTO.Prenom,
                            EntrepotId = updateUserRequestDTO.EntrepotId!,
                            MatriculeSOPAL = updateUserRequestDTO.
                            Role = "Magasinier",
                        };
                    }
                }
                serviceResponse.Success = false;
                serviceResponse.Message = "Erreur!";
                return serviceResponse;


            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = $"Erreur s'est produite : {ex.Message}";
                return serviceResponse;
            }
        }
    }
}
