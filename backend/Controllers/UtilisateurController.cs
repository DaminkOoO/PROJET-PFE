using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Dtos.User;
using backend.Helpers;
using backend.Interfaces;
using backend.Mappers;
using backend.Models;
using backend.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UtilisateurController : ControllerBase
    {
        //private readonly UserManager<Utilisateur> _userManager;
        private readonly IUtilisateurService _utilisateurService;
        public UtilisateurController(IUtilisateurService utilisateurService)
        {
            //_userManager = userManager;
            _utilisateurService = utilisateurService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserRequestDTO RegisterDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var createdUser = await _utilisateurService.CreateUtilisateur(RegisterDTO);
                if (createdUser.Succeeded)
                {
                    var roleResult = await _utilisateurService.SetUserRole(new Utilisateur
                    {
                        MatriculeSOPAL = RegisterDTO.MatriculeSopal,
                        Nom = RegisterDTO.Nom,
                        Prenom = RegisterDTO.Prenom,
                        Email = RegisterDTO.Email
                    }, RegisterDTO.Role);
                    if (roleResult.Succeeded)
                    {
                        return Ok(new RegisterUserResponseDTO
                        {
                            Role = RegisterDTO.Role,
                            Email = RegisterDTO.Email,
                            MatriculeSopal = RegisterDTO.MatriculeSopal,
                        });
                    }
                    else
                    {
                        return StatusCode(500, roleResult.Errors);
                    }
                }
                else
                {
                    return StatusCode(500, createdUser.Errors);
                }

            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var utilisateurs = await _utilisateurService.GetAllAsync();
            if (utilisateurs == null)
                return NotFound("Pas d'utilisateur enregistrer!");
            var utilisateursDTO = utilisateurs.Select(s => s.ToUtilisateurDTO()).ToList();
            return Ok(utilisateurs);
        }
        [HttpGet("{matriculeSopal}")]
        public async Task<IActionResult> GetById([FromRoute] string matriculeSopal)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var user = await _utilisateurService.GetByMatriculeAsync(matriculeSopal);
            if (user == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(user);
            }

        }
        [HttpPut]
        [Route("{matricule}")]
        public async Task<IActionResult> Update([FromRoute] string matricule, [FromBody] UpdateUserRequestDTO updateDto)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userModel = await _utilisateurService.UpdateUtilisateur(updateDto, matricule);

            if (userModel == null)
            {
                return NotFound();
            }
            var userDTO = userModel.ToUtilisateurDTO();
            userDTO.Role = updateDto.Role;
            return Ok(userDTO);
        }
        [HttpDelete]
        [Route("{matricule}")]
        public async Task<IActionResult> Delete([FromRoute] string matricule)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var deleting = await _utilisateurService.DeleteUtilisateur(matricule);
            return deleting ? NoContent() : NotFound();
        }

    }
}