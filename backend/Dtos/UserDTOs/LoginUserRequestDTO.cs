using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Dtos.UserDTOs
{
    public class LoginUserRequestDTO
    {
        [Required]
        public string MatriculeSopal { get; set; } = string.Empty;
    
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}