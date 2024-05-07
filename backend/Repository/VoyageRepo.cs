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
    public class VoyageRepo : IVoyageRepo
    {
        private readonly ApplicationDBContext _context;
        public VoyageRepo(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Voyage> Create(Voyage voyage)
        {
            await _context.Voyage.AddAsync(voyage);
            await _context.SaveChangesAsync();
            return voyage;
        }

        public async Task<List<Voyage>> GetAll(string? transporteurId, string? magasinierId, string? demandeurId)
        {
            IQueryable<Voyage> query = _context.Voyage.Include(voyage => voyage.Transporteurs);

            if (transporteurId != null)
            {
                query = query.Where(voyage => voyage.Transporteurs.Any(t => t.UserId == transporteurId));
            }

            if (magasinierId != null)
            {
                query = query.Where(voyage => voyage.MagasinierMatriculeSopal == magasinierId);
            }
            if (demandeurId != null)
            {
                query = query.Where(voyage => voyage.DemandeurMatriculeSopal == demandeurId);
            }


            return await query.ToListAsync();
        }

        public Voyage GetVoyageById(int id)
        {
            throw new NotImplementedException();
        }

        public Voyage Update(Voyage voyage)
        {
            throw new NotImplementedException();
        }
    }
}