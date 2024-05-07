using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Dtos.LocalisationDTO;
using backend.Dtos.TransporteurDTOs;
using backend.Dtos.User;
using backend.Helpers;
using backend.Interfaces;
using backend.Mappers;
using backend.Models;
using backend.Response;

namespace backend.Service
{
    public class TransporteurService : ITransporteurService
    {
        private readonly ITransporteurRepo _transporteurRepository;
        private readonly IUtilisateurService _utilisateurService;
        private readonly ILocalisationRepo _localisationRepo;

        public TransporteurService(ITransporteurRepo transporteurRepository, IUtilisateurService utilisateurService, ILocalisationRepo localisationRepo)
        {
            _transporteurRepository = transporteurRepository;
            _utilisateurService = utilisateurService;
            _localisationRepo = localisationRepo;
        }
        public async Task<ServiceResponse<TransporteurDTO>> CreateTransporteur(RegisterUserRequestDTO registerUserRequestDTO)
        {
            var serviceResponse = new ServiceResponse<TransporteurDTO>();

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

                    var transporteur = new Transporteur
                    {
                        UserId = registerUserRequestDTO.MatriculeSopal,
                        ExterneDuService = registerUserRequestDTO.ExterneDuService,
                        VehiculeMatricule = registerUserRequestDTO.VehiculeId
                    };
                    Console.WriteLine(transporteur.VehiculeMatricule);
                    await _transporteurRepository.CreateAsync(transporteur);
                    serviceResponse.Data = new TransporteurDTO
                    {
                        Email = registerUserRequestDTO.Email,
                        ExterneDuService = registerUserRequestDTO.ExterneDuService,
                        VehiculeId = registerUserRequestDTO.VehiculeId,
                        MatriculeSOPAL = registerUserRequestDTO.MatriculeSopal,
                        Nom = registerUserRequestDTO.Nom,
                        Prenom = registerUserRequestDTO.Prenom,
                        Role = "Demandeur"

                    };
                    serviceResponse.Success = true;
                    serviceResponse.Message = "Demandeur créé avec succès";
                    return serviceResponse;
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Erreur! Vérifier les champs";
                    Console.WriteLine(user.Message);
                    return serviceResponse;
                }

            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = $"Une erreur s'est produite au niveau du TransporteurService : {ex.Message}";
                return serviceResponse;
            }
        }

        public async Task<ServiceResponse<bool>> DeleteTransporteur(string matricule)
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
                //ce bloc de code vérifie si l'utilisateur est un transporteur ou non
                var doesExist = await _utilisateurService.IsExistRole(utilisateur.Data!.ToUtilisateur(), "Transporteur");
                if (doesExist.Success == false)
                {

                    return doesExist;
                }
                var transporteur = await _transporteurRepository.GetByIdAsync(matricule);
                if (transporteur != null)
                {
                    var deleteUser = await _transporteurRepository.DeleteAsync(matricule);
                    var deleteUtilisateur = await _utilisateurService.DeleteUtilisateur(matricule);
                    if (deleteUtilisateur.Success)
                    {
                        await _transporteurRepository.DeleteAsync(matricule);
                        serviceResponse.Data = true;
                        serviceResponse.Message = "Demandeur Supprimé avec succée!!";
                        return serviceResponse;
                    }
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
        public async Task<ServiceResponse<LocalisationDTO>> UpdatePostion(string matricule, LocalisationDTO localisationDTO)
        {
            ServiceResponse<LocalisationDTO> serviceResponse = new ServiceResponse<LocalisationDTO>();
            try
            {
                var transporteur = await _transporteurRepository.GetByIdAsync(matricule);
                var Localisation = await _localisationRepo.CreateAsync(new Localisation
                {
                    Lattitude = localisationDTO.Lattitude,
                    Longitude = localisationDTO.Longitude,
                    Date = DateTime.Now,
                    Transporteur = transporteur!,
                });
                var positionUpdate = await _transporteurRepository.UpdatePostion(matricule, Localisation);
                serviceResponse.Data = localisationDTO;
                serviceResponse.Success = true;
                serviceResponse.Message = $"Opération effectuer avec succèes!!";
                return serviceResponse;
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = $"Erreur s'est produite : {ex.Message}";
                return serviceResponse;
            }

        }
        public async Task<ServiceResponse<List<TransporteurDTO>?>> GetAllAsync(QueryObject TransporteurQueryObject)
        {
            var serviceResponse = new ServiceResponse<List<TransporteurDTO>?>();

            try
            {
                var utilisateurs = await _utilisateurService.GetAllAsync();
                var transporteurs = await _transporteurRepository.GetAllAsync(TransporteurQueryObject);
                var query = from t in transporteurs
                            join u in utilisateurs.Data!
                            on t.UserId equals u.MatriculeSOPAL
                            select new TransporteurDTO
                            {
                                Email = u.Email,
                                MatriculeSOPAL = t.UserId,
                                Nom = u.Nom,
                                Prenom = u.Prenom,
                                ExterneDuService = t.ExterneDuService,
                                Role = "Transporteur",

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

        public async Task<ServiceResponse<TransporteurDTO?>> GetByMatriculeAsync(UtilisateurDTO u)
        {
            var serviceResponse = new ServiceResponse<TransporteurDTO?>();
            try
            {

                var transporteur = await _transporteurRepository.GetByIdAsync(u.MatriculeSOPAL);
                if (u == null || transporteur == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Erreur! Utilisateur non existant!";
                    return serviceResponse;
                }
                serviceResponse.Success = true;
                serviceResponse.Data = new TransporteurDTO
                {
                    Email = u.Email,
                    MatriculeSOPAL = transporteur!.UserId,
                    ExterneDuService = transporteur.ExterneDuService,
                    VehiculeId = transporteur.VehiculeMatricule,
                    Nom = u.Nom,
                    Prenom = u.Prenom,
                    Role = "Transporteur",

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

        public async Task<ServiceResponse<TransporteurDTO?>> UpdateTransporteur(UpdateUserRequestDTO updateUserRequestDTO, string matricule)
        {
            var serviceResponse = new ServiceResponse<TransporteurDTO?>();
            try
            {
                var utilisateurResponse = await _utilisateurService.GetByMatriculeAsync(matricule);
                var transporteur = await _transporteurRepository.GetByIdAsync(matricule);

                if (!utilisateurResponse.Success || transporteur == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Erreur! Utilisateur non existant!";
                    return serviceResponse;
                }

                // Update the utilisateur
                var updatedUtilisateurResponse = await _utilisateurService.UpdateUtilisateur(updateUserRequestDTO, matricule);
                if (!updatedUtilisateurResponse.Success)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = updatedUtilisateurResponse.Message; // Pass through the error message
                    return serviceResponse;
                }

                // Update the transporteur
                var updatedTransporteur = await _transporteurRepository.UpdateAsync(matricule, updateUserRequestDTO);
                if (updatedTransporteur == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Erreur lors de la mise à jour du transporteur.";
                    return serviceResponse;
                }

                // Construct the response DTO
                serviceResponse.Success = true;
                serviceResponse.Message = "Transporteur mis à jour avec succès.";
                serviceResponse.Data = new TransporteurDTO
                {
                    Nom = updateUserRequestDTO.Nom,
                    Prenom = updateUserRequestDTO.Prenom,
                    ExterneDuService = updateUserRequestDTO.ExterneDuService,
                    MatriculeSOPAL = updatedUtilisateurResponse.Data!.MatriculeSopal,
                    Role = "Transporteur"
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
    }
}