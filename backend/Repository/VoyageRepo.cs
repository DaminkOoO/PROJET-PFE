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
INSERT INTO Voyage (IdVoyage, DateSortieBon, DateDepartVoyage, Etat, Poids, NbrePalette, Priorite, MagasinierDemandeurMatriculeSopal, MagasinierMatriculeSopal, TypeVoyageId)
VALUES (@IdVoyage, @DateSortieBon, @DateDepartVoyage, @Etat, @Poids, @NbrePalette,  @Priorite, @MagasinierDemandeurMatriculeSopal, @MagasinierMatriculeSopal, @TypeVoyageId); 
";

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
                    command.Parameters.AddWithValue("@DemandeurMatriculeSopal", voyage.DemandeurMatriculeSopal == "" ? null : voyage.DemandeurMatriculeSopal);
                    command.Parameters.AddWithValue("@Priorite", voyage.Priorite);
                    command.Parameters.AddWithValue("@MagasinierDemandeurMatriculeSopal", voyage.MagasinierDemandeurMatriculeSopal);
                    command.Parameters.AddWithValue("@MagasinierMatriculeSopal", voyage.MagasinierMatriculeSopal);
                    command.Parameters.AddWithValue("@TypeVoyageId", voyage.TypeVoyageId);
                    // Set other parameters as needed

                    // Execute the command and get the inserted identity (assuming it's an identity column)
                    int newVoyageId = Convert.ToInt32(await command.ExecuteScalarAsync());

                    // Optionally, retrieve the newly inserted voyage from the database and return it
                    // You would need another SQL query to fetch the newly inserted record

                    return voyage;
                }
            }
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

        public async Task<Voyage?> GetVoyageById(string id)
        {
            //return await _context.Magasinier.Include(m => m.Utilisateur).Include(m => m.Entrepot).FirstOrDefaultAsync(i => i.MatriculeSopal == matricule);
            return await _context.Voyage.Include(v => v.TypeVoyage)
            .Include(v => v.Demandeur).Include(v => v.Transporteurs)
            .Include(v => v.Magasinier)
            .Include(v => v.MagasinierDemandeur).FirstOrDefaultAsync(v => v.IdVoyage == id);
        }

        public Voyage Update(Voyage voyage)
        {
            throw new NotImplementedException();
        }
    }
}