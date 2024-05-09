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
    }
}