using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using backend.Dtos.User;
using backend.Dtos.UserDTOs;
using backend.Interfaces;
using backend.Mappers;
using backend.Models;
using backend.Response;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;

namespace backend.Service
{
    public class UtilisateurService : IUtilisateurService
    {
        private readonly IUtilisateurRepo _utilisateurRepository;
        private IPasswordHasher<Utilisateur> _passwordHasher;
        private readonly SignInManager<Utilisateur> _signinManager;
        private readonly IConfiguration _config;
        private readonly SymmetricSecurityKey _key;

        public UtilisateurService(IUtilisateurRepo utilisateurRepository, IPasswordHasher<Utilisateur> passwordHash, SignInManager<Utilisateur> signInManager, IConfiguration config)
        {
            _utilisateurRepository = utilisateurRepository;
            _passwordHasher = passwordHash;
            _signinManager = signInManager;
            _config = config;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SigningKey"]!));
        }

        public async Task<ServiceResponse<IdentityResult>> CreateUtilisateur(RegisterUserRequestDTO registerUserRequestDTO)
        {

            var serviceResponse = new ServiceResponse<IdentityResult>();
            if (registerUserRequestDTO == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Veuillez remplir tous les champs";
                return serviceResponse;
            }

            try
            {
                var userExists = await GetByMatriculeAsync(registerUserRequestDTO.MatriculeSopal);
                if (userExists.Success)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Utilisateur déja existe";
                    return serviceResponse;
                }
                var user = registerUserRequestDTO.ToUtilisateur();
                Console.WriteLine(user);
                user.PasswordHash = _passwordHasher.HashPassword(user, registerUserRequestDTO.Password!);
                var createdUser = await _utilisateurRepository.CreateAsync(user);
                if (createdUser.Succeeded)
                {
                    //To do tomorrow: add the actors
                    var roleResult = await _utilisateurRepository.AddToRole(user, registerUserRequestDTO.Role);

                    if (roleResult.Succeeded)
                    {

                        serviceResponse.Success = true;
                        serviceResponse.Message = "Utilisateur créé avec succès";
                        serviceResponse.Data = roleResult;
                        return serviceResponse;
                    }
                    else
                    {
                        serviceResponse.Success = false;
                        serviceResponse.Message = "Erreur lors de l'ajout du rôle";
                        return serviceResponse;
                    }
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = string.Join(", ", createdUser.Errors.Select(x => "Code " + x.Code + " Description" + x.Description));
                    return serviceResponse;
                }

            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = $"Une erreur s'est produite : {ex.Message}";
                return serviceResponse;
            }
        }

        public async Task<ServiceResponse<bool>> DeleteUtilisateur(string matricule)
        {
            var serviceResponse = new ServiceResponse<bool>();
            try
            {
                var utilisateur = await _utilisateurRepository.GetByMatriculeAsync(matricule);
                if (utilisateur == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Erreur! Utilisateur non existant!";
                    return serviceResponse;

                }
                var role = await _utilisateurRepository.GetUtilisateurRoles(utilisateur);


                var isDeletedFromRole = await _utilisateurRepository.RemoveFromRoleAsync(utilisateur, role[0]);
                if (!isDeletedFromRole.Succeeded)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Erreur! L'utilisateur n'appartient pas a cette role!";
                    return serviceResponse;
                }

                var result = await _utilisateurRepository.DeleteAsync(utilisateur);
                if (result.Succeeded)
                {
                    serviceResponse.Data = true;
                    serviceResponse.Message = "Utilisateur supprimé avec succès!";
                    return serviceResponse;
                }
                serviceResponse.Success = false;
                serviceResponse.Message = "Erreur! Utilisateur non supprimé!";
                return serviceResponse;
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = $"Une erreur s'est produite : {ex.Message}";
                return serviceResponse;
            }
        }

        public async Task<ServiceResponse<List<UtilisateurDTO>?>> GetAllAsync()
        {
            var serviceResponse = new ServiceResponse<List<UtilisateurDTO>?>();
            try
            {
                var utilisateurs = await _utilisateurRepository.GetAllAsync();
                var utilisateursDTO = utilisateurs.Select(s => s.ToUtilisateurDTO()).ToList();
                foreach (UtilisateurDTO utilisateurDTO in utilisateursDTO)
                {
                    Console.WriteLine(utilisateurDTO.MatriculeSOPAL);
                    var userRoles = await _utilisateurRepository.GetUtilisateurRoles(utilisateurDTO.ToUtilisateur());
                    utilisateurDTO.Role = userRoles[0];

                }
                if (utilisateurs == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Une erreur s'est produite";
                    return serviceResponse;
                }
                serviceResponse.Success = true;
                serviceResponse.Message = "";
                serviceResponse.Data = utilisateursDTO;
                return serviceResponse;
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = $"Une erreur s'est produite : {ex.Message}";
                return serviceResponse;
            }
        }

        public async Task<ServiceResponse<UtilisateurDTO?>> GetByMatriculeAsync(string matricule)
        {
            var serviceResponse = new ServiceResponse<UtilisateurDTO?>();
            try
            {
                var utilisateur = await _utilisateurRepository.GetByMatriculeAsync(matricule);



                if (utilisateur == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Utilisateur introuvable";
                    return serviceResponse;
                }
                var userRoles = await _utilisateurRepository.GetUtilisateurRoles(utilisateur!);
                var utilisateurDTO = utilisateur.ToUtilisateurDTO();
                utilisateurDTO.Role = userRoles[0];

                serviceResponse.Success = true;
                serviceResponse.Message = "";
                serviceResponse.Data = utilisateurDTO;
                return serviceResponse;
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = $"Une erreur s'est produite : {ex.Message}";
                return serviceResponse;
            }
        }
        public async Task<ServiceResponse<bool>> DeleteAdmin(string matricule)
        {
            var serviceResponse = new ServiceResponse<bool>();
            try
            {
                var utilisateur = await GetByMatriculeAsync(matricule);
                if (!utilisateur.Success)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Erreur! Utilisateur non existant!";
                    return serviceResponse;
                }
                var doesExist = await IsExistRole(utilisateur.Data!.ToUtilisateur(), "Admin");
                if (doesExist.Success == false)
                {

                    return doesExist;
                }
                var deleteUtilisateur = await DeleteUtilisateur(matricule);
                if (deleteUtilisateur.Success)
                {
                    serviceResponse.Data = true;
                    serviceResponse.Message = "Demandeur Supprimer avec succée!!";
                    return serviceResponse;
                }
                serviceResponse.Data = false;
                serviceResponse.Message = "Erreur!";
                return serviceResponse;
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = $"Erreur s'est produite : {ex.Message}";
                return serviceResponse;
            }
        }
        public async Task<ServiceResponse<bool>> IsExistRole(Utilisateur utilisateur, string role)
        {
            var serviceResponse = new ServiceResponse<bool>();
            try
            {
                var userRoles = await _utilisateurRepository.GetUtilisateurRoles(utilisateur);
                if (role == userRoles[0])
                {
                    serviceResponse.Success = true;
                    serviceResponse.Message = $"";
                    return serviceResponse;
                }
                serviceResponse.Success = false;
                serviceResponse.Message = $"L'utilisateur n'appartient pas a cette role!";
                return serviceResponse;
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = $"Une erreur s'est produite : {ex.Message}";
                return serviceResponse;
            }
        }
        public async Task<ServiceResponse<List<string>>> GetRoles()
        {
            var serviceResponse = new ServiceResponse<List<string>>();
            try
            {
                var roles = await _utilisateurRepository.GetRoles();
                if (roles == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Pas de roles";
                    return serviceResponse;
                }
                serviceResponse.Success = true;
                serviceResponse.Message = "";
                serviceResponse.Data = roles!;
                return serviceResponse;
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = $"Une erreur s'est produite : {ex.Message}";
                return serviceResponse;
            }
        }

        //use the SetRole while creating other users 
        public async Task<ServiceResponse<IdentityResult>> SetUserRole(Utilisateur utilisateur, string role)
        {
            await _utilisateurRepository.AddToRole(utilisateur, role);
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<Utilisateur?>> UpdateUtilisateur(UpdateUserRequestDTO updateUserRequestDTO, string matricule)
        {
            var serviceResponse = new ServiceResponse<Utilisateur?>();
            try
            {
                var userModel = await _utilisateurRepository.GetByMatriculeAsync(matricule);

                if (userModel == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Erreur! Utilisateur introuvable!";
                    return serviceResponse;
                }

                userModel.Nom = updateUserRequestDTO.Nom;
                userModel.Email = updateUserRequestDTO.Email;
                userModel.Prenom = updateUserRequestDTO.Prenom;

                if (updateUserRequestDTO.MotDePasse != null && updateUserRequestDTO.MotDePasse != "")
                    userModel.PasswordHash = _passwordHasher.HashPassword(userModel, updateUserRequestDTO.MotDePasse);
                var utilisateurUpdate = _utilisateurRepository.UpdateAsync(userModel);
                serviceResponse.Success = true;
                serviceResponse.Message = "Utilisateur Ajoutée!";
                serviceResponse.Data = userModel;
                return serviceResponse;
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = $"Une erreur s'est produite : {ex.Message}";
                return serviceResponse;
            }
        }
        private string CreateToken(Utilisateur user, string role)
        {

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email, user.Email!),
                new Claim(JwtRegisteredClaimNames.GivenName, user.UserName!),
                new Claim("role", role),
                new Claim("userId", user.Id)

            };

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds,
                Issuer = _config["JWT:Issuer"],
                Audience = _config["JWT:Audience"]
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
        public async Task<ServiceResponse<LoginUserResponseDTO>> Login(LoginUserRequestDTO LoginUserRequestDTO)
        {
            var serviceResponse = new ServiceResponse<LoginUserResponseDTO>();

            try
            {

                var user = await _utilisateurRepository.GetByMatriculeAsync(LoginUserRequestDTO.MatriculeSopal);
                if (user != null)
                {
                    var passwordCheck = await _signinManager.CheckPasswordSignInAsync(user, LoginUserRequestDTO.Password, false);
                    if (passwordCheck != null)
                    {
                        var userRoles = await _utilisateurRepository.GetUtilisateurRoles(user);
                        Console.WriteLine("****************************" + userRoles.Count);
                        var role = userRoles[userRoles.Count - 1];
                        serviceResponse.Success = true;
                        serviceResponse.Message = "Utilisateur Ajoutée!";
                        serviceResponse.Data = new LoginUserResponseDTO
                        {
                            MatriculeSopal = LoginUserRequestDTO.MatriculeSopal,
                            Token = CreateToken(user, role)
                        };
                        return serviceResponse;
                    }
                    else
                    {
                        serviceResponse.Success = false;
                        serviceResponse.Message = "Mot de passe incorrecte!";
                        return serviceResponse;
                    }
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Matricule introuvable";
                    return serviceResponse;
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = $"Une erreur s'est produite : {ex.Message}";
                return serviceResponse;
            }

        }

        public async Task<ServiceResponse<Utilisateur?>> UpdateToken(string matricule, string token)
        {
            ServiceResponse<Utilisateur?> serviceResponse = new ServiceResponse<Utilisateur?>();
            try
            {
                var userModel = await _utilisateurRepository.GetByMatriculeAsync(matricule);
                if (userModel == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Erreur! Utilisateur non existant!";
                    return serviceResponse;
                }
                userModel.TokenMessage = token;
                serviceResponse.Success = true;
                serviceResponse.Data = userModel;
                userModel.TokenMessage = token;
                var utilisateurUpdate = await _utilisateurRepository.UpdateAsync(userModel);
                serviceResponse.Message = "Token mis à jour avec succès.";
                return serviceResponse;
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = $"Erreur s'est produite : {ex.Message}";
                return serviceResponse;
            }
        }
    }
}