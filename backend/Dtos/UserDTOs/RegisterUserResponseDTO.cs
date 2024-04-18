using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Dtos.User
{
    public class RegisterUserResponseDTO
    {
        public string Email { get; set; } = string.Empty;
        public string MatriculeSopal { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }
}