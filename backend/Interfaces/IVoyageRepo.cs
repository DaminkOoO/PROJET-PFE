using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Models;

namespace backend.Interfaces
{
    public interface IVoyageRepo
    {
        Voyage Create(Voyage voyage);
        Voyage GetVoyageById(int id);
        List<Voyage> GetAll(string transporteurId, string magasinierId);
        Voyage Update(Voyage voyage);
        

    }
}