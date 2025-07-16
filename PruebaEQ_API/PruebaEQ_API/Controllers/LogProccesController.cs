using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PruebaEQ_API.Models;
using PruebaEQ_API.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaEQ_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogProccesController(ILogProccesService LogProccesService) : ControllerBase
    {
        private readonly ILogProccesService _logProccesService = LogProccesService;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LogProcces>>> GetLogProcces()
        {
            return await _logProccesService.GetAll();
        }

        // GET: api/LogProcces/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LogProcces>> GetLogProcces(int id)
        {
            var logProcces = await _logProccesService.Get(id);

            if (logProcces == null)
            {
                return NotFound();
            }

            return logProcces;
        }

        [HttpPost]
        public async Task<ActionResult<LogProcces>> PostLogProcces(LogProcces logProcces)
        {
            var result = await _logProccesService.CreateLogProcces(logProcces);
            if (result == null)
            {
                return NoContent();
            }
            return CreatedAtAction("GetLogProcces", new { id = logProcces.Id }, logProcces);
        }


    }
}
