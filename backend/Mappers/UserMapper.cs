using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Dtos.User;
using backend.Models;

namespace backend.Mappers
{
    public static class UserMapper
    {
        public static UtilisateurDTO ToUtilisateurDTO(this Utilisateur utilisateurModel)
        {
            return new UtilisateurDTO
            {
                Email = utilisateurModel.Email,
                MatriculeSOPAL = utilisateurModel.MatriculeSOPAL,
                Nom = utilisateurModel.Nom,
                Prenom = utilisateurModel.Prenom,
                Entrepot = utilisateurModel.Entrepot,
                Vehicule = utilisateurModel.Vehicule,
            };
        }
        public static Utilisateur ToUtilisateur(this UtilisateurDTO utilisateurDTO)
        {
            return new Utilisateur
            {
                Email = utilisateurDTO.Email,
                Prenom = utilisateurDTO.Prenom,
                Nom = utilisateurDTO.Nom,
                MatriculeSOPAL = utilisateurDTO.MatriculeSOPAL,
                
            };
        }
    }
}