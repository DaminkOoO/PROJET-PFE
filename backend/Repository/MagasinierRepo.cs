using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Data;
using backend.Dtos.MagasinierDTOs;
using backend.Dtos.User;
using backend.Helpers;
using backend.Interfaces;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Repository
{
    public class MagasinierRepo : IMagasinierRepo
    {
        private readonly ApplicationDBContext _context;
        public MagasinierRepo(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<Magasinier> CreateAsync(Magasinier magasinierModel)
        {
            await _context.Magasinier.AddAsync(magasinierModel);
            await _context.SaveChangesAsync();
            return magasinierModel;
        }

        public async Task<Magasinier?> DeleteAsync(string matricule)
        {
            var magasinierModel = await _context.Magasinier.FirstOrDefaultAsync(v => v.MatriculeSopal == matricule);

            if (magasinierModel == null)
            {
                return null;
            }

            _context.Magasinier.Remove(magasinierModel);
            await _context.SaveChangesAsync();
            return magasinierModel;
        }

        public async Task<List<Magasinier>> GetAllAsync(QueryObject queryObject)
        {
            var magasiniers = _context.Magasinier.Include(m => m.Utilisateur).Include(m => m.Entrepot).AsQueryable();
            // if (!string.IsNullOrWhiteSpace(queryObject.Marque))
            // {
            //     magasiniers = magasiniers.Where(v => v.Marque == queryObject.Marque);
            // }
            // if (!string.IsNullOrWhiteSpace(queryObject.Modele))
            // {
            //     magasiniers = magasiniers.Where(v => v.Modele == queryObject.Modele);
            // }
            return await magasiniers.Take(queryObject.PageSize).ToListAsync();
        }

        public async Task<Magasinier?> GetByIdAsync(string matricule)
        {
            return await _context.Magasinier.Include(m => m.Utilisateur).Include(m => m.Entrepot).FirstOrDefaultAsync(i => i.MatriculeSopal == matricule);

        }
        public async Task<Magasinier?> GetByEntropotAsync(string entrepotId)
        {
            return await _context.Magasinier.Include(m => m.Utilisateur).Include(m => m.Entrepot).FirstOrDefaultAsync(i => i.EntrepotId == entrepotId);

        }
        public async Task<Magasinier?> UpdateAsync(string matricule, UpdateUserRequestDTO MagasinierDto)
        {
            var existingMagasinier = await _context.Magasinier.FindAsync(matricule);

            if (existingMagasinier == null)
            {
                return null;
            }
            existingMagasinier.EntrepotId = MagasinierDto.EntrepotId!;
            await _context.SaveChangesAsync();
            return existingMagasinier;
        }

    }
}