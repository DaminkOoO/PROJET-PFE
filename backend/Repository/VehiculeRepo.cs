using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Data;
using backend.Dtos.VehiculeDTOs;
using backend.Helpers;
using backend.Interfaces;
using backend.Models;
using backend.Response;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace backend.Repository
{
    public class VehiculeRepo : IVehiculeRepo
    {
        private readonly ApplicationDBContext _context;
        public VehiculeRepo(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<Vehicule> CreateAsync(Vehicule VehiculeModel)
        {
            Console.WriteLine(VehiculeModel.Volume);
            await _context.Vehicule.AddAsync(VehiculeModel);
            await _context.SaveChangesAsync();
            return VehiculeModel;
        }

        public async Task<Vehicule?> DeleteAsync(string matricule)
        {
            var vehiculeModel = await _context.Vehicule.FirstOrDefaultAsync(v => v.Matricule == matricule);

            if (vehiculeModel == null)
            {
                return null;
            }

            _context.Vehicule.Remove(vehiculeModel);
            await _context.SaveChangesAsync();
            return vehiculeModel;
        }

        public async Task<List<Vehicule>> GetAllAsync(QueryObject queryObject)
        {
            var vehicules = _context.Vehicule.AsQueryable();
            // if (!string.IsNullOrWhiteSpace(queryObject.Marque))
            // {
            //     vehicules = vehicules.Where(v => v.Marque == queryObject.Marque);
            // }
            // if (!string.IsNullOrWhiteSpace(queryObject.Modele))
            // {
            //     vehicules = vehicules.Where(v => v.Modele == queryObject.Modele);
            // }
            return await vehicules.Take(queryObject.PageSize).ToListAsync();

        }

        public async Task<Vehicule?> GetByIdAsync(string matricule)
        {
            return await _context.Vehicule.FirstOrDefaultAsync(i => i.Matricule == matricule);
        }

        public async Task<bool> SwitchLibre(string matricule)
        {
            var vehicule = await _context.Vehicule.FirstOrDefaultAsync(v => v.Matricule == matricule);
            if (vehicule == null)
            {
                Console.WriteLine("Vehicule Introuvable");
                return false;
            }
            Console.WriteLine("************************** " + vehicule.Matricule + " **************************");
            vehicule.IsLibre = !vehicule.IsLibre;
            _context.Vehicule.Update(vehicule);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Vehicule?> UpdateAsync(string matricule, UpdateVehiculeRequestDTO VehiculeDto)
        {
            var existingVehicule = await _context.Vehicule.FirstOrDefaultAsync(v => v.Matricule == matricule);

            if (existingVehicule == null)
            {
                return null;
            }


            existingVehicule.Marque = VehiculeDto.Marque;
            existingVehicule.Modele = VehiculeDto.Modele;
            existingVehicule.Poids = VehiculeDto.Poids;
            existingVehicule.Volume = VehiculeDto.Volume;
            existingVehicule.Bloque = VehiculeDto.Bloque;

            _context.Vehicule.Update(existingVehicule);
            await _context.SaveChangesAsync();

            return existingVehicule;
        }
    }
}