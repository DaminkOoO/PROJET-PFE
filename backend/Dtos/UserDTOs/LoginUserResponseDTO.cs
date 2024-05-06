using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Dtos.UserDTOs
{
    public class LoginUserResponseDTO
    {
        public string MatriculeSopal { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;

    }
}