using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Dtos.User;
using backend.Models;

namespace backend.Dtos.MagasinierDTOs
{
    public class MagasinierDTO:UtilisateurDTO
    {
        public string EntrepotId { get; set; } = string.Empty;
    }
}