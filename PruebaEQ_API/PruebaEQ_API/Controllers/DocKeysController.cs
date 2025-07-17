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
    // Controlador para la gestión de claves asociadas a documentos (DocKeys).
    // Requiere autenticación mediante JWT.

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DocKeysController(IDocKeyService docKeyService) : ControllerBase
    {
        // Servicio inyectado que maneja la lógica de negocio para DocKeys
        private readonly IDocKeyService _docKeyService = docKeyService;

        // Obtener todas las claves registradas
        [HttpGet]
        public async Task<IEnumerable<DocKey>> GetDocKeys()
        {
            return await _docKeyService.GetAll();
        }

        // Obtener una clave específica por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<DocKey>> GetDocKey(int id)
        {
            var docKey = await _docKeyService.Get(id);
            if (docKey == null)
                return NotFound();

            return docKey;
        }

        // Actualizar una clave existente
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDocKey(int id, DocKey docKey)
        {
            if (id != docKey.Id)
                return BadRequest();

            await _docKeyService.UpdateDocKey(id, docKey);
            return Ok();
        }

        // Crear una nueva clave
        [HttpPost]
        public async Task<ActionResult<DocKey>> PostDocKey(DocKey docKey)
        {
            await _docKeyService.CreateDocKey(docKey);
            return CreatedAtAction("GetDocKey", new { id = docKey.Id }, docKey);
        }

        // Eliminar una clave existente
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDocKey(int id)
        {
            var result = await _docKeyService.DeleteDocKey(id);
            if (!result)
                return NotFound();

            return NoContent();
        }
    }

}
