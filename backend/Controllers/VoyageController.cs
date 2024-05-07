using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Dtos.EntrepotDTOs;
using backend.Dtos.VoyageDTOs;
using backend.Helpers;
using backend.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/Voyage")]
    [Authorize]

    public class VoyageController : ControllerBase
    {
        private readonly IEntrepotService _entrepotService;
        private readonly ITransporteurService _transporteurService;
        private readonly IVoyageService _voyageService;
        public VoyageController(IEntrepotService entrepotService, ITransporteurService transporteurService, IVoyageService voyageService)
        {
            _entrepotService = entrepotService;
            _transporteurService = transporteurService;
            _voyageService = voyageService;
        }
        [HttpPost]
        public async Task<ActionResult<VoyageDTO>> CreateVoyage([FromBody] VoyageDTO voyageDTO){
            var voyage = await _voyageService.CreateVoyage(voyageDTO);
            return voyage.Success ? Ok(voyage.Data) : BadRequest(voyage.Message);
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<VoyageDTO>>> GetAllVoyages()
        {
            var voyages = await _voyageService.GetAllVoyages(null, null, null);
            return voyages.Success ? Ok(voyages.Data) : BadRequest(voyages.Message);
        }
        [HttpGet("GetVoyagesTranspoteur/{matricule}")]
        [Authorize(Roles = "Admin,Transporteur")]
        public async Task<ActionResult<List<VoyageDTO>>> GetAllVoyagesTransporteur([FromRoute] string matricule)
        {
            var voyages = await _voyageService.GetAllVoyages(matricule, null, null);
            return voyages.Success ? Ok(voyages.Data) : BadRequest(voyages.Message);
        }
        [HttpGet("GetVoyagesDemandeur/{matricule}")]
        [Authorize(Roles = "Admin,Demandeur")]
        public async Task<ActionResult<List<VoyageDTO>>> GetAllVoyagesDemandeur([FromRoute] string matricule)
        {
            var voyages = await _voyageService.GetAllVoyages(null, null, matricule);
            return voyages.Success ? Ok(voyages.Data) : BadRequest(voyages.Message);
        }
        [HttpGet("GetVoyagesMagasinier/{matricule}")]
        [Authorize(Roles = "Admin,Magasinier")]
        public async Task<ActionResult<List<VoyageDTO>>> GetAllVoyagesMagasinier([FromRoute] string matricule)
        {
            var voyages = await _voyageService.GetAllVoyages(null, matricule, null);
            return voyages.Success ? Ok(voyages.Data) : BadRequest(voyages.Message);
        }
    }
}