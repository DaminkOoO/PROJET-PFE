using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Data;
using backend.Dtos.DemandeurDTO;
using backend.Dtos.User;
using backend.Helpers;
using backend.Interfaces;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Repository
{
    public class DemandeurRepo : IDemandeurRepo
    {
        private readonly ApplicationDBContext _context;
        public DemandeurRepo(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<Demandeur> CreateAsync(Demandeur DemandeurModel)
        {
            await _context.Demandeur.AddAsync(DemandeurModel);
            await _context.SaveChangesAsync();
            return DemandeurModel;
        }

        public async Task<Demandeur?> DeleteAsync(string matricule)
        {
            var demandeurModel = await _context.Demandeur.FirstOrDefaultAsync(v => v.UserId == matricule);

            if (demandeurModel == null)
            {
                return null;
            }

            _context.Demandeur.Remove(demandeurModel);
            await _context.SaveChangesAsync();
            return demandeurModel;
        }

        public async Task<List<Demandeur>> GetAllAsync(QueryObject queryObject)
        {
            var demandeurs = _context.Demandeur.AsQueryable();

            // if (!string.IsNullOrWhiteSpace(queryObject.Marque))
            // {
            //     demandeurs = demandeurs.Where(v => v.Marque == queryObject.Marque);
            // }
            // if (!string.IsNullOrWhiteSpace(queryObject.Modele))
            // {
            //     demandeurs = demandeurs.Where(v => v.Modele == queryObject.Modele);
            // }
            return await demandeurs.Take(queryObject.PageSize).ToListAsync();

        }

        public async Task<Demandeur?> GetByIdAsync(string matricule)
        {
            return await _context.Demandeur.FindAsync(matricule);
        }

        public async Task<Demandeur?> UpdateAsync(string matricule, UpdateUserRequestDTO DemandeurDto)
        {
            var existingDemandeur = await _context.Demandeur.FindAsync(matricule);

            if (existingDemandeur == null)
            {
                return null;
            }
            existingDemandeur.Longitude = DemandeurDto.Longitude;
            existingDemandeur.Lattitude = DemandeurDto.Lattitude;
            existingDemandeur.IsMagasinier = DemandeurDto.Role=="Magasinier";
            await _context.SaveChangesAsync();
            return existingDemandeur;
        }
    }
}