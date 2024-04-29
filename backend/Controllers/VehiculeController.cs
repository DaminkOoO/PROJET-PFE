using System;
using System.Threading.Tasks;
using backend.Dtos.VehiculeDTOs;
using backend.Helpers;
using backend.Interfaces;
using backend.Service;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/Vehicule")]
    [ApiController]
    public class VehiculeController : ControllerBase
    {
        private readonly IVehiculeService _vehiculeService;

        public VehiculeController(IVehiculeService vehiculeService)
        {
            _vehiculeService = vehiculeService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateVehiculeRequestDTO createVehicule)
        {
            var response = await _vehiculeService.CreateVehicule(createVehicule);
            if (response.Success)
            {
                return CreatedAtAction(nameof(GetById), new { matricule = response.Data!.Matricule }, response.Data);
            }
            return BadRequest(response.Message);
        }

        [HttpGet("{matricule}")]
        public async Task<IActionResult> GetById([FromRoute] string matricule)
        {
            var response = await _vehiculeService.GetVehiculeById(matricule);
            if (response.Success)
            {
                return Ok(response.Data);
            }
            return NotFound(response.Message);
        }
        [HttpGet("disponible")]
        public async Task<IActionResult> GetAllLibre()
        {
            var response = await _vehiculeService.GetAllVehiculesLibre();
            return response.Success ? Ok(response.Data) : BadRequest(response.Message);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            var response = await _vehiculeService.GetAllAsync(query);
            return response.Success ? Ok(response.Data) : BadRequest(response.Message);
        }

        [HttpPut("{matricule}")]
        public async Task<IActionResult> Update([FromRoute] string matricule, [FromBody] UpdateVehiculeRequestDTO updateDto)
        {
            var response = await _vehiculeService.UpdateVehicule(matricule, updateDto);
            if (response.Success)
            {
                return Ok(response.Data);
            }
            return NotFound(response.Message);
        }

        [HttpDelete("{matriculeSopal}")]
        public async Task<IActionResult> Delete([FromRoute] string matriculeSopal)
        {
            var response = await _vehiculeService.DeleteVehicule(matriculeSopal);
            return response.Success ? NoContent() : NotFound(response.Message);
        }
    }
}