using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Dtos.User;
using backend.Models;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace backend.Mappers
{
    public static class UserMapper
    {
        public static UtilisateurDTO ToUtilisateurDTO(this Utilisateur utilisateurModel)
        {
            return new UtilisateurDTO
            {
                Email = utilisateurModel.Email,
                MatriculeSOPAL = utilisateurModel.Id,
                Nom = utilisateurModel.Nom,
                Prenom = utilisateurModel.Prenom,
            };
        }
        public static Utilisateur ToUtilisateur(this UtilisateurDTO utilisateurDTO)
        {
            return new Utilisateur
            {
                Email = utilisateurDTO.Email,
                Prenom = utilisateurDTO.Prenom,
                Nom = utilisateurDTO.Nom,
                Id = utilisateurDTO.MatriculeSOPAL,

            };
        }
        public static Utilisateur ToUtilisateur(this RegisterUserRequestDTO registerUserRequestDTO)
        {

            return new Utilisateur
            {
                Email = registerUserRequestDTO.Email,
                Nom = registerUserRequestDTO.Nom,
                Prenom = registerUserRequestDTO.Prenom,
                Id = registerUserRequestDTO.MatriculeSopal,
                UserName = registerUserRequestDTO.MatriculeSopal + registerUserRequestDTO.Prenom,
            };

        }
    }
}