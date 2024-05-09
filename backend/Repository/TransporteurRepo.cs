using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Data;
using backend.Dtos.TransporteurDTOs;
using backend.Dtos.User;
using backend.Helpers;
using backend.Interfaces;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Repository
{
    public class TransporteurRepo : ITransporteurRepo
    {
        private readonly ApplicationDBContext _context;
        public TransporteurRepo(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<Transporteur> CreateAsync(Transporteur TransporteurModel)
        {
            TransporteurModel.Localisation = new Localisation
            {
                Date = DateTime.Now,
                Lattitude = 0,
                Longitude = 0,
            };
            await _context.Transporteur.AddAsync(TransporteurModel);
            await _context.SaveChangesAsync();
            return TransporteurModel;
        }

        public async Task<Transporteur?> DeleteAsync(string matricule)
        {
            var TransporteurModel = await _context.Transporteur.FirstOrDefaultAsync(v => v.UserId == matricule);

            if (TransporteurModel == null)
            {
                return null;
            }

            _context.Transporteur.Remove(TransporteurModel);
            await _context.SaveChangesAsync();
            return TransporteurModel;
        }

        public async Task<List<Transporteur>> GetAllAsync(QueryObject queryObject)
        {
            var transporteurs = _context.Transporteur.Include(t => t.Vehicule).Include(t => t.Localisation).Include(t => t.Utilisateur).AsQueryable();
            // if (!string.IsNullOrWhiteSpace(queryObject.Marque))
            // {
            //     transporteurs = transporteurs.Where(v => v.Marque == queryObject.Marque);
            // }
            // if (!string.IsNullOrWhiteSpace(queryObject.Modele))
            // {
            //     transporteurs = transporteurs.Where(v => v.Modele == queryObject.Modele);
            // }
            return await transporteurs.Take(queryObject.PageSize).ToListAsync();
        }

        public async Task<Transporteur?> GetByIdAsync(string matricule)
        {
            return await _context.Transporteur.Include(t => t.Vehicule).Include(t => t.Localisation).Include(t => t.Utilisateur).FirstOrDefaultAsync(t => t.UserId == matricule);
        }

        public Task<List<Transporteur>> GetTransporteursDisponible()
        {
             var transporteurs = _context.Transporteur.Include(t => t.Vehicule).
             Include(t => t.Localisation).Include(t => t.Utilisateur).Where(t=>t.Active );
             return transporteurs.ToListAsync();
        }

        public async Task<Transporteur?> UpdateAsync(string matricule, UpdateUserRequestDTO TransporteurDto)
        {
            var existingTransporteur = await _context.Transporteur.FindAsync(matricule);

            if (existingTransporteur == null)
            {
                return null;
            }
            existingTransporteur.ExterneDuService = TransporteurDto.ExterneDuService;
            existingTransporteur.VehiculeMatricule = TransporteurDto.VehiculeId;
            _context.Transporteur.Update(existingTransporteur);
            await _context.SaveChangesAsync();
            return existingTransporteur;
        }

        public async Task<Transporteur?> UpdatePostion(string matricule, Localisation localisation)
        {
            var existingTransporteur = await _context.Transporteur.FindAsync(matricule);

            if (existingTransporteur == null)
            {
                return existingTransporteur;
            }
            existingTransporteur.LocalisationId = localisation.Id;
            _context.Transporteur.Update(existingTransporteur);
            await _context.SaveChangesAsync();
            return existingTransporteur;
        }
    }
}