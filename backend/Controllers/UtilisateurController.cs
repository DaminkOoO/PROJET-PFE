using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Dtos.DemandeurDTOs;
using backend.Dtos.LocalisationDTO;
using backend.Dtos.User;
using backend.Dtos.UserDTOs;
using backend.Dtos.VehiculeDTOs;
using backend.Helpers;
using backend.Interfaces;
using backend.Mappers;
using backend.Models;
using backend.Repository;
using backend.Response;
using backend.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    [Route("api/user")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class UtilisateurController : ControllerBase
    {
        private readonly IUtilisateurService _utilisateurService;
        private readonly IDemandeurService _demandeurService;
        private readonly IMagasinierService _magasinierService;

        private readonly ITransporteurService _transporteurService;
        private readonly IVehiculeService _vehiculeService;

        public UtilisateurController(IUtilisateurService utilisateurService,
         IDemandeurService demandeurService, IMagasinierService magasinierService,
          ITransporteurService transporteurService, IVehiculeService vehiculeService)
        {

            _utilisateurService = utilisateurService;
            _demandeurService = demandeurService;
            _magasinierService = magasinierService;
            _transporteurService = transporteurService;
            _vehiculeService = vehiculeService;
        }
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserRequestDTO RegisterDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (RegisterDTO.Role == "Demandeur")
                {
                    var createdUser = await _demandeurService.CreateDemandeur(RegisterDTO);
                    return createdUser.Success ? Ok(createdUser.Data) : BadRequest(createdUser.Message);
                }
                if (RegisterDTO.Role == "Admin")
                {

                    var createdUser = await _utilisateurService.CreateUtilisateur(RegisterDTO);
                    return createdUser.Success ? Ok(createdUser.Data) : BadRequest(createdUser.Message);

                }
                if (RegisterDTO.Role == "Transporteur")
                {
                    var createdUser = await _transporteurService.CreateTransporteur(RegisterDTO);
                    if (createdUser.Success)
                    {
                        var switchLibre = await _vehiculeService.SwitchLibre(RegisterDTO.VehiculeId);
                        if (!switchLibre.Success)
                        {
                            return BadRequest(switchLibre.Message);
                        }
                        return Ok(createdUser.Data);
                    }
                    else
                    {
                        return BadRequest(createdUser.Message);
                    }



                }
                if (RegisterDTO.Role == "Magasinier")
                {
                    var createdUser = await _magasinierService.CreateMagasinier(RegisterDTO);
                    return createdUser.Success ? Ok(createdUser.Data) : BadRequest(createdUser.Message);
                }
                return StatusCode(500);


            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserRequestDTO LoginDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var isLogged = await _utilisateurService.Login(LoginDTO);
            return isLogged.Success ? Ok(isLogged.Data) : Unauthorized(isLogged.Message);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var utilisateursDTO = await _utilisateurService.GetAllAsync();

            return utilisateursDTO != null ? Ok(utilisateursDTO) : NotFound();
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

                if (user.Data!.Role == "Demandeur")
                {
                    var demandeurDTO = await _demandeurService.GetByMatriculeAsync(user.Data);
                    return demandeurDTO != null ? Ok(demandeurDTO) : NotFound();
                }
                if (user.Data.Role == "Admin")
                {


                    return user != null ? Ok(user) : NotFound();

                }
                if (user.Data.Role == "Transporteur")
                {
                    var transporteurDTO = await _transporteurService.GetByMatriculeAsync(user.Data);

                    return transporteurDTO != null ? Ok(transporteurDTO) : NotFound();
                }
                if (user.Data.Role == "Magasinier")
                {
                    var magasinierDTO = await _magasinierService.GetByMatriculeAsync(user.Data);
                    return magasinierDTO != null ? Ok(magasinierDTO) : NotFound();
                }
                return NotFound();
            }

        }
        [HttpPut]
        [Route("{matricule}")]
        public async Task<IActionResult> Update([FromRoute] string matricule, [FromBody] UpdateUserRequestDTO updateDto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _utilisateurService.GetByMatriculeAsync(matricule);
            if (user == null)
            {
                return NotFound();
            }

            if (user.Data!.Role == "Demandeur")
            {
                var updatedDemandeur = await _demandeurService.UpdateDemandeur(updateDto, matricule);
                return updatedDemandeur.Success ? Ok(updatedDemandeur.Data) : BadRequest(updatedDemandeur.Message);
            }
            else if (user.Data.Role == "Admin")
            {
                var updatedAdmin = await _utilisateurService.UpdateUtilisateur(updateDto, matricule);
                return updatedAdmin.Success ? Ok(updatedAdmin.Data) : BadRequest(updatedAdmin.Message);
            }
            else if (user.Data.Role == "Transporteur")
            {
                var transporteur = await _transporteurService.GetByMatriculeAsync(user.Data);
                if (transporteur == null)
                {
                    return NotFound("Transporteur not found");
                }

                if (transporteur.Data!.VehiculeId != updateDto.VehiculeId)
                {
                    var switchNew = await _vehiculeService.SwitchLibre(updateDto.VehiculeId!);
                    var switchOld = await _vehiculeService.SwitchLibre(transporteur.Data.VehiculeId!);

                    if (!switchNew.Success || !switchOld.Success)
                    {
                        if (!switchNew.Success)
                        {
                            await _vehiculeService.SwitchLibre(transporteur.Data.VehiculeId!);
                        }
                        if (!switchOld.Success)
                        {
                            await _vehiculeService.SwitchLibre(updateDto.VehiculeId!);
                        }
                        return BadRequest("Failed to switch vehicle availability");
                    }
                }

                var updatedTransporteur = await _transporteurService.UpdateTransporteur(updateDto, matricule);
                return updatedTransporteur.Success ? Ok(updatedTransporteur.Data) : BadRequest(updatedTransporteur.Message);
            }
            else if (user.Data.Role == "Magasinier")
            {
                var updatedMagasinier = await _magasinierService.UpdateMagasinier(updateDto, matricule);
                return updatedMagasinier.Success ? Ok(updatedMagasinier.Data) : BadRequest(updatedMagasinier.Message);
            }
            else
            {
                return BadRequest("Invalid role");
            }
        }
        [HttpDelete]
        [Route("{matricule}")]
        public async Task<IActionResult> Delete([FromRoute] string matricule, [FromBody] DeleteUserRequest deleteUser)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            Console.WriteLine(deleteUser.Role);
            if (deleteUser.Role == "Demandeur")
            {
                var deleting = await _demandeurService.DeleteDemandeur(matricule);

                return deleting.Success ? NoContent() : NotFound();
            }

            if (deleteUser.Role == "Transporteur")

            {
                var libreVehicule = await _vehiculeService.SwitchLibre(deleteUser.VehiculeId);
                var deleting = await _transporteurService.DeleteTransporteur(matricule);

                return deleting.Success ? NoContent() : NotFound(deleting.Message);
            }
            if (deleteUser.Role == "Magasinier")
            {
                var deleting = await _magasinierService.DeleteMagasinier(matricule);
                return deleting.Success ? NoContent() : NotFound(deleting.Message);
            }
            if (deleteUser.Role == "Admin")
            {

                var deletingAdmin = await _utilisateurService.DeleteAdmin(matricule);
                return deletingAdmin.Success ? NoContent() : NotFound(deletingAdmin.Message);
            }
            return NotFound();
        }
        [HttpPut]
        [Route("transporteur/{matricule}")]
        [Authorize(Roles = "Transporteur")]
        public async Task<IActionResult> UpdateLocation([FromRoute] string matricule, [FromBody] LocalisationDTO localisationDTO)
        {
            var updated = await _transporteurService.UpdatePostion(matricule, localisationDTO);
            return updated.Success ? Ok(updated.Data) : BadRequest(updated.Message);
        }
    }
}