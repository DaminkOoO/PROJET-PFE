using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Dtos.VoyageDTOs;
using backend.Interfaces;
using backend.Mappers;
using backend.Models;
using backend.Response;
using Hubs;
using Microsoft.AspNetCore.SignalR;

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
            const decimal R = 6371;

            decimal dLat = (lat2 - lat1) * (decimal)Math.PI / 180;
            decimal dLon = (lon2 - lon1) * (decimal)Math.PI / 180;

            decimal a = (decimal)Math.Sin((double)(dLat / 2)) * (decimal)Math.Sin((double)(dLat / 2)) +
                       (decimal)Math.Cos((double)(lat1 * (decimal)Math.PI / 180)) * (decimal)Math.Cos((double)(lat2 * (decimal)Math.PI / 180)) *
                       (decimal)Math.Sin((double)(dLon / 2)) * (decimal)Math.Sin((double)(dLon / 2));

            decimal c = 2 * (decimal)Math.Atan2((double)Math.Sqrt((double)a), (double)Math.Sqrt((double)(1 - a)));

            decimal distance = R * c;
            return distance;
        }

        public async Task<ServiceResponse<VoyageDTO>> CreateVoyage(VoyageDTO voyageDTO)
        {
            ServiceResponse<VoyageDTO> serviceResponse = new ServiceResponse<VoyageDTO>();
            try
            {
                //TODO: find Transporteur by id from VoyageDTO and type Voyage By Nom;
                var voyage = voyageDTO.ToVoyage();
                voyage.MagasinierMatriculeSopal = voyageDTO.MagasinierMatriculeSopal;
                voyage.Magasinier = await _magasinierRepo.GetByIdAsync(voyageDTO.MagasinierMatriculeSopal);
                voyage.Transporteurs[0] = await _transporteurRepo.GetByIdAsync(voyageDTO.TransporteursId);
                if (!voyageDTO.DemandeurMatriculeSopal.Equals(""))
                {
                    voyage.DemandeurMatriculeSopal = voyageDTO.DemandeurMatriculeSopal;
                    voyage.Demandeur = await _demandeurRepo.GetByIdAsync(voyageDTO.DemandeurMatriculeSopal);
                    voyage.Priorite = CalculateDistance(voyage.Demandeur!.Lattitude,
                                        voyage.Demandeur!.Longitude,
                                        voyage.Transporteurs[0].Localisation!.Lattitude,
                                        voyage.Transporteurs[0].Localisation!.Longitude
                                        );
                }
                voyage.Priorite = CalculateDistance(voyage.Magasinier!.Entrepot.Lattitude,
                                    voyage.Magasinier.Entrepot.Longitude,
                                    voyage.Transporteurs[0].Localisation!.Lattitude,
                                    voyage.Transporteurs[0].Localisation!.Longitude);
                var createVoyage = await _voyageRepo.Create(voyage);

                return serviceResponse;
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = $"Une erreur s'est produite : {ex.Message}";
                return serviceResponse;
            }
        }

        public Task<ServiceResponse<List<VoyageDTO>>> GetAllVoyages(string? transporteurId, string? magasinierId, string? demandeurId)
        {
            //TODO: populate the list if it's null or empty 


            throw new NotImplementedException();
        }
    }
}