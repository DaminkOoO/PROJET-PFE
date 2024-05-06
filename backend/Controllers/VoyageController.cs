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
    [Route("api/Voyage")]
    [Authorize]

    public class VoyageController : ControllerBase
    {
        private readonly IEntrepotService _entrepotService;
        private readonly ITransporteurService _transporteurService;
        public VoyageController(IEntrepotService entrepotService, ITransporteurService transporteurService)
        {
            _entrepotService = entrepotService;
            _transporteurService = transporteurService;
        }
        



    }
}