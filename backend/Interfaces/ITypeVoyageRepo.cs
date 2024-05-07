using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Models;

namespace backend.Interfaces
{
    public interface ITypeVoyageRepo
    {
        Task<TypeVoyage> CreateAsync(string TypeVoyage);
        Task<TypeVoyage?> DeleteAsync(string TypeVoyage);
        Task<List<TypeVoyage>> GetAllAsync();
        Task<TypeVoyage?> GetByIdAsync(string TypeVoyage);
    }
}