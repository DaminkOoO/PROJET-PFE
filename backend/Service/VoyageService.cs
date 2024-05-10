using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using backend.Dtos.VoyageDTOs;
using backend.Helpers;
using backend.Interfaces;
using backend.Mappers;
using backend.Models;
using backend.Response;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Data.SqlClient;

namespace backend.Service
{
    public class VoyageService : IVoyageService
    {
        private readonly IVoyageRepo _voyageRepo;
        private readonly ITypeVoyageRepo _typeVoyageRepo;
        private readonly ITransporteurRepo _transporteurRepo;
        private readonly IMagasinierRepo _magasinierRepo;
        private readonly IDemandeurRepo _demandeurRepo;
        private List<Voyage> voyages = new List<Voyage>();

        public VoyageService(IVoyageRepo voyageRepo, ITypeVoyageRepo typeVoyageRepo, ITransporteurRepo transporteurRepo, IMagasinierRepo magasinierRepo, IDemandeurRepo demandeurRepo)
        {
            _typeVoyageRepo = typeVoyageRepo;
            _voyageRepo = voyageRepo;
            _transporteurRepo = transporteurRepo;
            _magasinierRepo = magasinierRepo;
            _demandeurRepo = demandeurRepo;
        }

        public void Enqueue(Voyage voyage)
        {
            voyages.Add(voyage);
            voyages.Sort((x, y) => x.Priorite.CompareTo(y.Priorite));
        }

        public Voyage Dequeue()
        {
            if (IsEmpty())
            {
                throw new InvalidOperationException("Queue is empty");
            }

            Voyage voyage = voyages[0];
            voyages.RemoveAt(0);
            return voyage;
        }

        public void ChangePriorite(string voyageId, int newPriorite)
        {
            Voyage voyage = voyages.Find(v => v.IdVoyage == voyageId)!;
            if (voyage != null)
            {
                voyage.Priorite = newPriorite;
                voyages.Sort((x, y) => x.Priorite.CompareTo(y.Priorite));
            }
            else
            {
                throw new KeyNotFoundException("Voyage not found");
            }
        }

        private bool IsEmpty()
        {
            return voyages.Count == 0;
        }


        public decimal CalculateDistance(decimal lat1, decimal lon1, decimal lat2, decimal lon2)
        {
            try
            {
                const decimal R = 6371;

                decimal dLat = (lat2 - lat1) * (decimal)Math.PI / 180;
                decimal dLon = (lon2 - lon1) * (decimal)Math.PI / 180;

                decimal a = (decimal)Math.Sin((double)(dLat / 2)) * (decimal)Math.Sin((double)(dLat / 2)) +
                           (decimal)Math.Cos((double)(lat1 * (decimal)Math.PI / 180)) * (decimal)Math.Cos((double)(lat2 * (decimal)Math.PI / 180)) *
                           (decimal)Math.Sin((double)(dLon / 2)) * (decimal)Math.Sin((double)(dLon / 2));

                decimal c = 2 * (decimal)Math.Atan2((double)Math.Sqrt((double)a), (double)Math.Sqrt((double)(1 - a)));

                decimal distance = R * c;
                return Math.Round(distance, 17);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }


        }

        private Transporteur GetClosestTransporteur(List<Transporteur> transporteurs, decimal lat, decimal lon)
        {
            try
            {
                Transporteur closest = new();
                Console.WriteLine("GetClosestTransporteur 1");
                decimal closestDistance = Decimal.MaxValue;
                Console.WriteLine("GetClosestTransporteur 2");

                foreach (Transporteur transporteur in transporteurs)
                {

                    if (transporteur.Localisation == null)
                    {
                        throw new Exception("Localisation null");
                    }
                    decimal distance = CalculateDistance(lat, lon, transporteur.Localisation!.Lattitude, transporteur.Localisation.Longitude);

                    if (distance < closestDistance && transporteur.Active)
                    {

                        closest = transporteur;

                        closestDistance = distance;

                    }
                }

                return closest;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null!;
            }

        }

        public async Task<ServiceResponse<Voyage>> CreateVoyage(VoyageDTO voyageDTO)
        {
            ServiceResponse<Voyage> serviceResponse = new ServiceResponse<Voyage>();
            try
            {
                var doesExist = await _voyageRepo.GetVoyageById(voyageDTO.IdVoyage);
                if (doesExist != null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = $"Voyage déja existant";
                    return serviceResponse;
                }
                var voyage = voyageDTO.ToVoyage();
                var typeVoyage = await _typeVoyageRepo.GetByIdAsync(voyageDTO.TypeVoyage);
                typeVoyage ??= await _typeVoyageRepo.CreateAsync(voyageDTO.TypeVoyage);
                voyage.TypeVoyageId = typeVoyage.IdTypeVoyage;

                var magasinier = await _magasinierRepo.GetByEntropotAsync(voyageDTO.MagasinierMatriculeSopal);

                if (magasinier == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = $"Magasinier introuvable";
                    return serviceResponse;
                }

                voyage.MagasinierMatriculeSopal = magasinier.Utilisateur.Id;

                decimal lat1 = 0;
                decimal lon1 = 0;
                if (!string.IsNullOrEmpty(voyageDTO.DemandeurMatriculeSopal))
                {
                    var Demandeur = await _demandeurRepo.GetByIdAsync(voyageDTO.DemandeurMatriculeSopal);
                    if (Demandeur != null)
                    {
                        voyage.DemandeurMatriculeSopal = Demandeur.UserId;
                        lat1 = Demandeur.Lattitude;
                        lon1 = Demandeur.Longitude;
                    }
                    else
                    {

                        var MagasinierDemandeur = await _magasinierRepo.GetByIdAsync(voyageDTO.MagasinierDemandeurMatriculeSopal);
                        if (MagasinierDemandeur == null)
                        {
                            serviceResponse.Success = false;
                            serviceResponse.Message = $"Erreur:::";
                            return serviceResponse;
                        }
                        voyage.MagasinierDemandeurMatriculeSopal = voyageDTO.MagasinierDemandeurMatriculeSopal;
                        lat1 = MagasinierDemandeur.Entrepot.Lattitude;
                        lon1 = MagasinierDemandeur.Entrepot.Longitude;
                    }
                }

                // Get all Transporteurs
                var transporteurs = await _transporteurRepo.GetTransporteursDisponible();
                if (transporteurs == null || transporteurs.Count == 0)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = $"Pas de transporteur";
                    return serviceResponse;
                }

                // Assign closest Transporteur
                var closestTransporteur = GetClosestTransporteur(transporteurs, lat1, lon1);
                if (closestTransporteur == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = $"Pas de transporteur actif";
                    return serviceResponse;
                }
                voyage.TransporteurId = closestTransporteur.UserId;
                await SendNotification(closestTransporteur, "Nouveau voyage affecté!", "Vous avez un nouveau affecté! Merci de vérifier la liste des voyages");
                // Calculate and assign Priorite
                voyage.Priorite = CalculateDistance(lat1, lon1, closestTransporteur.Localisation.Lattitude, closestTransporteur.Localisation.Longitude);
                // Create Voyage
                var createdVoyage = await _voyageRepo.Create(voyage);
                Enqueue(createdVoyage);
                // Prepare response
                serviceResponse.Data = voyage;
                return serviceResponse;
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = $"An error occurred: {ex.Message}";

                // Log the stack trace
                Console.WriteLine("Detailed Stack Trace:");
                Console.WriteLine(ex.StackTrace);

                // Log inner exceptions if available
                Console.WriteLine(ex.Source);
                Console.WriteLine(ex.InnerException);

                return serviceResponse;
            }
        }
        private async Task<string> SendNotification(Transporteur transporteur, string title, string body)
        {
            try
            {
                FirebaseApp firebaseApp = FirebaseApp.DefaultInstance;
                if (firebaseApp == null)
                {
                    GoogleCredential googleCredential;
                    using (var stream = new FileStream(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "pushnotif-e2732-firebase-adminsdk-2fir6-63f1e18b85.json"), FileMode.Open, FileAccess.Read))
                    {
                        googleCredential = GoogleCredential.FromStream(stream);
                    }

                    firebaseApp = FirebaseApp.Create(new AppOptions()
                    {
                        Credential = googleCredential
                    });
                }

                FirebaseMessaging messaging = FirebaseMessaging.DefaultInstance;

                var message = new Message()
                {
                    Notification = new Notification
                    {
                        Title = title,
                        Body = body
                    },
                    Token = transporteur.Token // Assuming Token is the FCM token of the recipient
                };

                // Send the message
                string response = await messaging.SendAsync(message);

                return response;
            }
            catch (Exception ex)
            {
                // Handle exceptions here
                Console.WriteLine($"Error sending notification: {ex.Message}");
                return null;
            }
        }

        public async Task<ServiceResponse<List<VoyageDTO>>> GetAllVoyages(string? transporteurId, string? magasinierId, string? demandeurId)
        {
            //TODO: populate the list if it's null or empty 
            ServiceResponse<List<VoyageDTO>> serviceResponse = new ServiceResponse<List<VoyageDTO>>();

            try
            {
                // Call the repository method to get the list of voyages
                var voyages = await _voyageRepo.GetAll(transporteurId, magasinierId, demandeurId);

                // Check if the result is null or empty
                if (voyages == null || voyages.Count == 0)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "No voyages found.";
                    return serviceResponse;
                }

                // Map the list of Voyage to list of VoyageDTO
                var voyageDTOs = voyages.Select(voyage => new VoyageDTO
                {
                    IdVoyage = voyage.IdVoyage,
                    DateSortieBon = voyage.DateSortieBon,
                    Etat = voyage.Etat,
                    MagasinierDemandeurMatriculeSopal = voyage.MagasinierDemandeurMatriculeSopal,

                    // Map other properties as needed
                }).ToList();

                serviceResponse.Data = voyageDTOs;
                serviceResponse.Success = true;
                serviceResponse.Message = "Voyages retrieved successfully.";
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = $"An error occurred: {ex.Message}";

                // Log the error if needed
            }

            return serviceResponse;
        }


    }
}