using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Data;
using backend.Interfaces;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Repository
{
    public class TypeVoyageRepo:ITypeVoyageRepo
    {
        private readonly ApplicationDBContext _context;
        public TypeVoyageRepo(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<TypeVoyage> CreateAsync(string TypeVoyage)
        {
            var TypeVoyageModel = new TypeVoyage
            {
                Libelle = TypeVoyage,
            };
            await _context.TypeVoyage.AddAsync(TypeVoyageModel);
            await _context.SaveChangesAsync();
            return TypeVoyageModel;
        }

        public async Task<TypeVoyage?> DeleteAsync(string TypeVoyage)
        {
            var TypeVoyageModel = await _context.TypeVoyage.FirstOrDefaultAsync(v => v.Libelle == TypeVoyage);

            if (TypeVoyageModel == null)
            {
                return null;
            }

            _context.TypeVoyage.Remove(TypeVoyageModel);
            await _context.SaveChangesAsync();
            return TypeVoyageModel;
        }

        public async Task<List<TypeVoyage>> GetAllAsync()
        {
            var TypeVoyages = await _context.TypeVoyage.ToListAsync();
            return TypeVoyages;
        }

        public async Task<TypeVoyage?> GetByIdAsync(string TypeVoyage)
        {
            return await _context.TypeVoyage.FirstOrDefaultAsync(v => v.Libelle == TypeVoyage);

        }
    }
}