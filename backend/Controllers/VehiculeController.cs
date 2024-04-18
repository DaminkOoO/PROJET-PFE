using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Dtos.VehiculeDTOs;
using backend.Helpers;
using backend.Interfaces;
using backend.Mappers;
using backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/Vehicule")]
    [ApiController]
    public class VehiculeController : ControllerBase
    {
        private readonly IVehiculeRepo _vehiculeRepo;
        public VehiculeController(IVehiculeRepo VehiculeRepo)
        {
            _vehiculeRepo = VehiculeRepo;
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateVehiculeRequestDTO createVehicule)
        {

            var vehiculeModel = createVehicule.ToVehiculeFromCreateDTO();
            if (await _vehiculeRepo.GetByIdAsync(vehiculeModel.Matricule) == null)
            {
                await _vehiculeRepo.CreateAsync(vehiculeModel);
                return CreatedAtAction(nameof(GetById), new { matricule = vehiculeModel.Matricule }, vehiculeModel.ToVehiculeDTO());

            }
            return BadRequest("Vehicule déja existante");


        }

        [HttpGet("{matricule}")]
        public async Task<IActionResult> GetById([FromRoute] string matricule)
        {
            var vehicule = await _vehiculeRepo.GetByIdAsync(matricule);
            if (vehicule == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(vehicule.ToVehiculeDTO());
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var stocks = await _vehiculeRepo.GetAllAsync(query);
            var stockDto = stocks.Select(s => s.ToVehiculeDTO()).ToList();
            return Ok(stockDto);
        }
        [HttpPut]
        [Route("{matricule}")]
        public async Task<IActionResult> Update([FromRoute] string matricule, [FromBody] UpdateVehiculeRequestDTO updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var vehiculeModel = await _vehiculeRepo.UpdateAsync(matricule, updateDto);

            if (vehiculeModel == null)
            {
                return NotFound();
            }

            return Ok(vehiculeModel.ToVehiculeDTO());
        }

        [HttpDelete]
        [Route("{matriculeSopal}")]
        public async Task<IActionResult> Delete([FromRoute] string matriculeSopal)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var vehiculeModel = await _vehiculeRepo.DeleteAsync(matriculeSopal);
            if (vehiculeModel == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}