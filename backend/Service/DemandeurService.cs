using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Dtos.DemandeurDTO;
using backend.Dtos.DemandeurDTOs;
using backend.Dtos.User;
using backend.Helpers;
using backend.Interfaces;
using backend.Mappers;
using backend.Models;
using backend.Response;
using Microsoft.AspNetCore.DataProtection.KeyManagement.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace backend.Service
{
    public class DemandeurService : IDemandeurService
    {
        private readonly IDemandeurRepo _DemandeurRepository;
        private readonly IUtilisateurService _utilisateurService;

        public DemandeurService(IDemandeurRepo DemandeurRepository, IUtilisateurService utilisateurService)
        {
            _DemandeurRepository = DemandeurRepository;
            _utilisateurService = utilisateurService;
        }

        public async Task<ServiceResponse<DemandeurDTO>> CreateDemandeur(RegisterUserRequestDTO registerUserRequestDTO)
        {
            var serviceResponse = new ServiceResponse<DemandeurDTO>();
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
                Console.WriteLine(user.Message);
                if (user.Success)
                {
                    await _DemandeurRepository.CreateAsync(new Demandeur
                    {
                        UserId = registerUserRequestDTO.MatriculeSopal,
                        Lattitude = registerUserRequestDTO.Lattitude,
                        Longitude = registerUserRequestDTO.Longitude,
                        IsMagasinier = registerUserRequestDTO.Role == "Magasinier"

                    });
                    serviceResponse.Data = new DemandeurDTO
                    {
                        Email = registerUserRequestDTO.Email,
                        Lattitude = registerUserRequestDTO.Lattitude,
                        Longitude = registerUserRequestDTO.Longitude,
                        MatriculeSOPAL = registerUserRequestDTO.MatriculeSopal,
                        Role = "Demandeur"

                    };
                    serviceResponse.Success = true;
                    serviceResponse.Message = "Demandeur créé avec succès";
                    return serviceResponse;
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = user.Message;
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

        public async Task<ServiceResponse<bool>> DeleteDemandeur(string matricule)
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
                var doesExist = await _utilisateurService.IsExistRole(utilisateur.Data!.ToUtilisateur(),"Demandeur");
                if(doesExist.Success==false){
                     
                    return doesExist;
                }
                var deleteUtilisateur = await _utilisateurService.DeleteUtilisateur(matricule);
                if (deleteUtilisateur.Success)
                {
                    await _DemandeurRepository.DeleteAsync(matricule);
                    serviceResponse.Data = true;
                    serviceResponse.Message = "Demandeur Supprimer avec succée!!";
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

        public async Task<ServiceResponse<List<DemandeurDTO>?>> GetAllAsync(QueryObject DemandeurQueryObject)
        {
            var serviceResponse = new ServiceResponse<List<DemandeurDTO>?>();

            try
            {
                var utilisateurs = await _utilisateurService.GetAllAsync();
                var demandeurs = await _DemandeurRepository.GetAllAsync(DemandeurQueryObject);
                var query = from d in demandeurs
                            join u in utilisateurs.Data!
                            on d.UserId equals u.MatriculeSOPAL
                            select new DemandeurDTO
                            {
                                Email = u.Email,
                                MatriculeSOPAL = d.UserId,
                                Nom = u.Nom,
                                Prenom = u.Prenom,
                                Lattitude = d.Lattitude,
                                Longitude = d.Longitude,
                                Role = "Demandeur",
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

        public async Task<ServiceResponse<DemandeurDTO?>> GetByMatriculeAsync(UtilisateurDTO u)
        {
            var serviceResponse = new ServiceResponse<DemandeurDTO?>();
            try
            {
                var d = await _DemandeurRepository.GetByIdAsync(u.MatriculeSOPAL);
                if (u == null || d == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Erreur! Utilisateur non existant!";
                    return serviceResponse;
                }
                serviceResponse.Success = true;
                serviceResponse.Data = new DemandeurDTO
                {
                    Email = u.Email,
                    MatriculeSOPAL = d.UserId,
                    Nom = u.Nom,
                    Prenom = u.Prenom,
                    Lattitude = d.Lattitude,
                    Longitude = d.Longitude,
                    Role = "Demandeur",
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

        public async Task<ServiceResponse<DemandeurDTO?>> UpdateDemandeur(UpdateUserRequestDTO updateUserRequestDTO, string matricule)
        {
            var serviceResponse = new ServiceResponse<DemandeurDTO?>();
            try
            {
                var u = await _utilisateurService.GetByMatriculeAsync(matricule);
                var d = await _DemandeurRepository.GetByIdAsync(matricule);
                if (!u.Success || d == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Erreur! Utilisateur non existant!";
                    return serviceResponse;
                }
                var utilisateur = await _utilisateurService.UpdateUtilisateur(updateUserRequestDTO, matricule);
                if (utilisateur.Success)
                {
                    var demandeur = await _DemandeurRepository.UpdateAsync(matricule, updateUserRequestDTO);
                    if (demandeur != null)
                    {
                        serviceResponse.Success = true;
                        serviceResponse.Message = "";
                        serviceResponse.Data = new DemandeurDTO
                        {

                            Lattitude = updateUserRequestDTO.Lattitude,
                            Longitude = updateUserRequestDTO.Longitude,
                            Role = "Demandeur",
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