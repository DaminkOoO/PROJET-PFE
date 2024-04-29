using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Dtos.TransporteurDTOs
{
    public class UpdateTransporteurRequestDTO
    {
        public string? VehiculeId { get; set; } = string.Empty;
        public bool ExterneDuService { get; set; } = false;
    }
}