using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Dtos.LocalisationDTOs;
using backend.Dtos.TransporteurDTOs;
using backend.Dtos.User;
using backend.Dtos.VoyageDTOs;
using backend.Helpers;
using backend.Interfaces;
using backend.Mappers;
using backend.Models;
using backend.Response;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.IdentityModel.Tokens;

namespace backend.Service
{
    public class TransporteurService : ITransporteurService
    {
        private readonly ITransporteurRepo _transporteurRepository;
        private readonly IUtilisateurService _utilisateurService;
        private readonly ILocalisationRepo _localisationRepo;
        private readonly IVoyageService _voyageService;
        private readonly IVoyageRepo _voyageRepo;

        public TransporteurService(ITransporteurRepo transporteurRepository, IUtilisateurService utilisateurService, ILocalisationRepo localisationRepo, IVoyageService voyageService, IVoyageRepo voyageRepo)
        {
            _transporteurRepository = transporteurRepository;
            _utilisateurService = utilisateurService;
            _localisationRepo = localisationRepo;
            _voyageService = voyageService;
            _voyageRepo = voyageRepo;
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

                    var localisation = new Localisation
                    {
                        Date = DateTime.Now,
                        Lattitude = 0,
                        Longitude = 0,

                    };
                    var transporteur = new Transporteur
                    {
                        UserId = registerUserRequestDTO.MatriculeSopal,
                        ExterneDuService = registerUserRequestDTO.ExterneDuService,
                        VehiculeMatricule = registerUserRequestDTO.VehiculeId,
                        Localisation = localisation
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
                if (localisationDTO.Lattitude == transporteur!.Localisation!.Lattitude && localisationDTO.Longitude == transporteur.Localisation.Longitude)
                {
                    serviceResponse.Success = true;
                    serviceResponse.Message = $"Opération effectuer avec succèes!!";
                    return serviceResponse;
                }
                TransporteurLocalisation.TransporteurLocalisations.Add(
                    new TransporteurLocalisation
                    {
                        TransporteurId = matricule,
                        Localisation = new Localisation
                        {
                            Lattitude = transporteur!.Localisation!.Lattitude,
                            Longitude = transporteur!.Localisation!.Longitude,
                            Date = DateTime.Now,
                        }
                    }
                );
                var Localisation = await _localisationRepo.CreateAsync(new Localisation
                {
                    Lattitude = localisationDTO.Lattitude,
                    Longitude = localisationDTO.Longitude,
                    Date = DateTime.Now,

                });


                var positionUpdate = await _transporteurRepository.UpdatePostion(matricule, Localisation);
                //En cours de livraison
                var listVoyage = await _voyageRepo.GetAll(matricule, null, null);
                if (listVoyage != null)
                {
                    var voyageActive = listVoyage.Where(v => v.Etat == "En cours de livraison").FirstOrDefault();

                    decimal distance = 0;
                    if (voyageActive != null)
                    {

                        if (voyageActive.Demandeur != null)
                        {
                            Console.WriteLine("\n\n\n\n\ntest");
                            distance = _voyageService.CalculateDistance(voyageActive.Demandeur.Lattitude, voyageActive.Demandeur.Longitude, localisationDTO.Lattitude, localisationDTO.Longitude);
                            Console.WriteLine("Distance" + distance);
                            if (distance <= 0.2m)
                            {
                                await _voyageService.SendNotification(voyageActive.Demandeur.Utilisateur, "Votre voyage", "Voyage");
                                await _voyageService.UpdateVoyageState(new VoyageDTO
                                {
                                    IdVoyage = voyageActive.IdVoyage,
                                }, "Terminé");
                            }
                        }
                        else
                        {
                            Console.WriteLine("\n\n\n\n\n\n" + voyageActive.MagasinierDemandeur.MatriculeSopal);
                            if (voyageActive.MagasinierDemandeur != null)
                            {

                                distance = _voyageService.CalculateDistance(voyageActive.MagasinierDemandeur.Entrepot.Lattitude, voyageActive.MagasinierDemandeur.Entrepot.Longitude, localisationDTO.Lattitude, localisationDTO.Longitude);
                                Console.WriteLine("\n\n\n\n\nDistance:" + distance);
                                if (distance <= 0.2m)
                                {
                                    await _voyageService.SendNotification(voyageActive.MagasinierDemandeur.Utilisateur, "Votre voyage", "Voyage");
                                    await _voyageService.UpdateVoyageState(new VoyageDTO
                                    {
                                        IdVoyage = voyageActive.IdVoyage,
                                    }, "Terminé");
                                }
                            }
                        }

                    }
                }
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
        public async Task<ServiceResponse<Transporteur?>> UpdateToken(string matricule, string token)
        {
            ServiceResponse<Transporteur?> serviceResponse = new ServiceResponse<Transporteur?>();
            try
            {
                var updatedUser = await _transporteurRepository.UpdateToken(matricule, token);
                if (updatedUser == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Erreur! Utilisateur non existant!";
                    return serviceResponse;
                }
                serviceResponse.Success = true;
                serviceResponse.Data = updatedUser;
                serviceResponse.Message = "Token mis à jour avec succès.";
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
class TransporteurLocalisation
{
    public string TransporteurId { get; set; } = string.Empty;
    public Localisation Localisation { get; set; } = new Localisation();
    public static List<TransporteurLocalisation> TransporteurLocalisations { get; set; } = new List<TransporteurLocalisation>();
}