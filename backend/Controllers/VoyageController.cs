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
        public VoyageController(IEntrepotService entrepotService)
        {
            _entrepotService = entrepotService;
        }



    }
}