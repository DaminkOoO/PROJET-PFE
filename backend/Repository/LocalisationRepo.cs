using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Data;
using backend.Interfaces;
using backend.Models;

namespace backend.Repository
{
    public class LocalisationRepo : ILocalisationRepo
    {
        private readonly ApplicationDBContext _context;
        public LocalisationRepo(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<Localisation> CreateAsync(Localisation LocalisationModel)
        {
            await _context.Localisation.AddAsync(LocalisationModel);
            await _context.SaveChangesAsync();
            Console.WriteLine(LocalisationModel);
            return LocalisationModel;
        }
    }
}