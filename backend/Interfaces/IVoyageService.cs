using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Dtos.VoyageDTOs;
using backend.Models;
using backend.Response;

namespace backend.Interfaces
{
    public interface IVoyageService
    {
        Task<ServiceResponse<Voyage>> CreateVoyage(VoyageDTO voyageDTO);

        Task<ServiceResponse<List<VoyageDTO>>> GetAllVoyages(string? transporteurId, string? magasinierId, string? demandeurId);
        Task<ServiceResponse<Voyage>> UpdateVoyageState(VoyageDTO voyageDTO, string state);
        Task<ServiceResponse<PositionVoyageDTO>> GetVoyagePosition(string voyageId);
        decimal CalculateDistance(decimal lat1, decimal lon1, decimal lat2, decimal lon2);
        Task<string> SendNotification(Utilisateur utilisateur, string title, string body);
    }
}