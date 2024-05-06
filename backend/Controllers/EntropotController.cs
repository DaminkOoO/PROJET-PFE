using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Dtos.EntrepotDTOs;
using backend.Helpers;
using backend.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/Entrepot")]
    [Authorize]

    public class EntropotController : ControllerBase
    {
        private readonly IEntrepotService _entrepotService;
        public EntropotController(IEntrepotService entrepotService)
        {
            _entrepotService = entrepotService;
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]

        public async Task<ActionResult<List<EntrepotDTO>?>> GetAllAsync([FromQuery] QueryObject queryObject)
        {
            var entrepots = await _entrepotService.GetAllAsync(queryObject);
            return Ok(entrepots);
        }
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<EntrepotDTO?>> GetById([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entrepot = await _entrepotService.GetByIdAsync(id);
            if (entrepot == null)
            {
                return NotFound();
            }
            return Ok(entrepot);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<EntrepotDTO>> CreateEntrepot([FromBody] RegisterEntrepotDTO registerEntrepotRequestDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newEntrepot = await _entrepotService.CreateEntrepot(registerEntrepotRequestDTO);
            if (!newEntrepot.Success)
                return BadRequest(newEntrepot.Message);
            return CreatedAtAction(nameof(GetById), new { id = newEntrepot.Data!.Id }, newEntrepot.Data);
        }
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<EntrepotDTO>> UpdateEntrepot([FromRoute] string id, [FromBody] UpdateEntrepotRequestDTO updateEntrepotRequestDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var updatedEntrepot = await _entrepotService.UpdateEntrepot(updateEntrepotRequestDTO, id);
            if (updatedEntrepot == null)
            {
                return NotFound();
            }
            return Ok(updatedEntrepot);
        }
        [HttpDelete("{matricule}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteEntrepot([FromRoute] string matricule)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _entrepotService.DeleteEntrepot(matricule);
            if (!result.Data)
            {
                return NotFound();
            }
            return NoContent();
        }

    }
}