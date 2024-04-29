using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Dtos.User;
using backend.Models;

namespace backend.Dtos.TransporteurDTOs
{
    public class TransporteurDTO : UtilisateurDTO
    {
        public string? VehiculeId { get; set; } = string.Empty;
        public bool ExterneDuService { get; set; } = false;
        public bool IsLibre {get; set; }

    }
}