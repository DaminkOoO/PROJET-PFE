using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Dtos.EntrepotDTOs;
using backend.Helpers;
using backend.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/Entrepot")]
    public class EntropotController : ControllerBase
    {
        private readonly IEntrepotService _entrepotService;
        public EntropotController(IEntrepotService entrepotService)
        {
            _entrepotService = entrepotService;
        }
        [HttpGet]
        public async Task<ActionResult<List<EntrepotDTO>?>> GetAllAsync([FromQuery] QueryObject queryObject)
        {
            var entrepots = await _entrepotService.GetAllAsync(queryObject);
            return Ok(entrepots);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<EntrepotDTO?>> GetByIdAsync([FromRoute] string id)
        {
            var entrepot = await _entrepotService.GetByIdAsync(id);
            if (entrepot == null)
            {
                return NotFound();
            }
            return Ok(entrepot);
        }
        [HttpPost]
        public async Task<ActionResult<EntrepotDTO>> CreateEntrepot([FromBody] RegisterEntrepotDTO registerEntrepotRequestDTO)
        {
            var newEntrepot = await _entrepotService.CreateEntrepot(registerEntrepotRequestDTO);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = newEntrepot.Id }, newEntrepot);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<EntrepotDTO>> UpdateEntrepot([FromRoute] string id, [FromBody] UpdateEntrepotRequestDTO updateEntrepotRequestDTO)
        {
            var updatedEntrepot = await _entrepotService.UpdateEntrepot(updateEntrepotRequestDTO, id);
            if (updatedEntrepot == null)
            {
                return NotFound();
            }
            return Ok(updatedEntrepot);
        }
        [HttpDelete("{matricule}")]
        public async Task<IActionResult> DeleteEntrepot([FromRoute] string matricule)
        {
            var result = await _entrepotService.DeleteEntrepot(matricule);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

    }
}