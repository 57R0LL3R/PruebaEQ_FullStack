using Microsoft.AspNetCore.Authorization;
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
    // Controlador para consultar o registrar logs de procesamiento de archivos.
    // Todas las rutas requieren autenticación JWT.

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LogProccesController(ILogProcessService LogProccesService) : ControllerBase
    {
        private readonly ILogProcessService _logProccesService = LogProccesService;

        // Obtener todos los registros de procesamiento
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LogProcess>>> GetLogProcces()
        {
            return await _logProccesService.GetAll();
        }

        // Obtener un log específico por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<LogProcess>> GetLogProcces(int id)
        {
            var logProcces = await _logProccesService.Get(id);
            if (logProcces == null)
                return NotFound();

            return logProcces;
        }

        // Registrar un nuevo log de procesamiento
        [HttpPost]
        public async Task<ActionResult<LogProcess>> PostLogProcces(LogProcess logProcces)
        {
            var result = await _logProccesService.CreateLogProcces(logProcces);
            if (result == null)
                return NoContent();

            return CreatedAtAction("GetLogProcces", new { id = logProcces.Id }, logProcces);
        }
    }

}
