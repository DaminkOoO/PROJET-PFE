using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Dtos.UserDTOs
{
    public class DeleteUserRequest
    {
        [Required]
        public string Role { get; set; } = string.Empty;
        public string VehiculeId { get; set; } = string.Empty;
        public string EntrepotId { get; set; } = string.Empty;
    }
}