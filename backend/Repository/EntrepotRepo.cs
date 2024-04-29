using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Data;
using backend.Dtos.EntrepotDTOs;
using backend.Helpers;
using backend.Interfaces;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Repository
{
    public class EntrepotRepo : IEntrepotRepo
    {
        private readonly ApplicationDBContext _context;
        public EntrepotRepo(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<Entrepot> CreateAsync(Entrepot EntrepotModel)
        {
            await _context.Entrepot.AddAsync(EntrepotModel);
            await _context.SaveChangesAsync();
            return EntrepotModel;
        }

        public async Task<Entrepot?> DeleteAsync(string id)
        {
            var entrepotModel = await _context.Entrepot.FindAsync(id);

            if (entrepotModel == null)
            {
                return null;
            }

            _context.Entrepot.Remove(entrepotModel);
            await _context.SaveChangesAsync();
            return entrepotModel;
        }

        public async Task<List<Entrepot>> GetAllAsync(QueryObject QueryObject)
        {
            var entrepots = _context.Entrepot.AsQueryable();
            return await entrepots.Take(QueryObject.PageSize).ToListAsync();

        }

        public async Task<Entrepot?> GetByIdAsync(string id)
        {
            return await _context.Entrepot.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<Entrepot?> UpdateAsync(string id, UpdateEntrepotRequestDTO EntrepotDto)
        {
            var existingEntrepot = await _context.Entrepot.FindAsync(id);
            if(existingEntrepot == null){
                return null;
            }
            existingEntrepot.Nom = EntrepotDto.Nom;
            existingEntrepot.Longitude = EntrepotDto.Longitude;
            existingEntrepot.Lattitude = EntrepotDto.Lattitude;
            _context.Entrepot.Update(existingEntrepot);
            await _context.SaveChangesAsync();
            return existingEntrepot;
        }
    }
}