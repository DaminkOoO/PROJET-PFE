using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Models;

namespace backend.Interfaces
{
    public interface IVoyageRepo
    {
        Task<Voyage> Create(Voyage voyage);
        Task<Voyage?> GetVoyageById(string id);
        Task<List<Voyage>> GetAll(string? transporteurId, string? magasinierId, string? magasinierName);
        Voyage Update(Voyage voyage);


    }
}