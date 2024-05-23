using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Threading.Tasks;
using backend.Data;
using backend.Interfaces;
using backend.Models;
using Microsoft.Data.SqlClient;
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
            string query = @"
        INSERT INTO Voyage (IdVoyage, DateSortieBon, DateDepartVoyage, Etat, Poids, NbrePalette, Priorite, MagasinierDemandeurMatriculeSopal, MagasinierMatriculeSopal, TypeVoyageId,TransporteurId)
        VALUES (@IdVoyage, @DateSortieBon, @DateDepartVoyage, @Etat, @Poids, @NbrePalette,  @Priorite, @MagasinierDemandeurMatriculeSopal, @MagasinierMatriculeSopal, @TypeVoyageId,@TransporteurId); 
        SELECT SCOPE_IDENTITY();"; // This retrieves the last identity value inserted in the current scope

            using (var connection = new SqlConnection("Data Source=DESKTOP-MJ7O2KC\\SQLEXPRESS;Initial Catalog=sopal2;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    // Set parameters from the voyage object
                    command.Parameters.AddWithValue("@IdVoyage", voyage.IdVoyage);
                    command.Parameters.AddWithValue("@DateSortieBon", voyage.DateSortieBon == DateTime.MinValue ? SqlDateTime.Null : voyage.DateSortieBon);
                    command.Parameters.AddWithValue("@DateDepartVoyage", voyage.DateDepartVoyage == DateTime.MinValue ? SqlDateTime.Null : voyage.DateDepartVoyage);
                    command.Parameters.AddWithValue("@Etat", voyage.Etat);
                    command.Parameters.AddWithValue("@Poids", voyage.Poids);
                    command.Parameters.AddWithValue("@NbrePalette", voyage.NbrePalette);
                    command.Parameters.AddWithValue("@Priorite", voyage.Priorite);
                    command.Parameters.AddWithValue("@MagasinierDemandeurMatriculeSopal", voyage.MagasinierDemandeurMatriculeSopal);
                    command.Parameters.AddWithValue("@MagasinierMatriculeSopal", voyage.MagasinierMatriculeSopal);
                    command.Parameters.AddWithValue("@TypeVoyageId", voyage.TypeVoyageId);
                    command.Parameters.AddWithValue("@TransporteurId", voyage.TransporteurId);

                    // Execute the command and get the inserted identity
                    int newVoyageId = Convert.ToInt32(await command.ExecuteScalarAsync());

                    // Optionally, retrieve the newly inserted voyage from the database
                    // You would need another SQL query to fetch the newly inserted record based on the newVoyageId

                    // Return the original voyage object or the newly inserted voyage
                    return voyage;
                }
            }
        }


        public async Task<List<Voyage>> GetAll(string? transporteurId, string? magasinierId, string? demandeurId)
        {
            IQueryable<Voyage> query = _context.Voyage.Include(v => v.TypeVoyage)
            .Include(v => v.Demandeur).Include(v => v.Transporteur).ThenInclude(t => t.Localisation)
            .Include(v => v.Magasinier)
            .Include(v => v.MagasinierDemandeur).ThenInclude(m => m!.Entrepot);

            if (transporteurId != null)
            {
                query = query.Where(voyage => voyage.TransporteurId == transporteurId);
            }

            if (magasinierId != null)
            {
                query = query.Where(voyage => voyage.MagasinierMatriculeSopal == magasinierId || voyage.MagasinierDemandeurMatriculeSopal == magasinierId);
            }
            if (demandeurId != null)
            {
                query = query.Where(voyage => voyage.DemandeurMatriculeSopal == demandeurId);
            }


            return await query.ToListAsync();
        }

        public async Task<Voyage?> GetVoyageById(string id)
        {
            //return await _context.Magasinier.Include(m => m.Utilisateur).Include(m => m.Entrepot).FirstOr => i.MatriculeSopal == matricule);
            return await _context.Voyage.Include(v => v.TypeVoyage)
            .Include(v => v.Demandeur).Include(v => v.Transporteur).ThenInclude(t => t.Localisation)
            .Include(v => v.Magasinier).ThenInclude(m => m.Entrepot)
            .Include(v => v.MagasinierDemandeur).ThenInclude(m => m!.Entrepot).FirstOrDefaultAsync(v => v.IdVoyage == id);
        }

        public async Task<Voyage> Update(Voyage voyage)
        {
            var existingVoyage = await _context.Voyage.FindAsync(voyage.IdVoyage);

            if (existingVoyage == null)
            {
                return null!;
            }
            existingVoyage = voyage;
            _context.Voyage.Update(existingVoyage);
            await _context.SaveChangesAsync();
            return existingVoyage;
        }
    }
}