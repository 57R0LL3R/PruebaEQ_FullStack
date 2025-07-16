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
    public class DocKeysController(IDocKeyService docKeyService) : ControllerBase
    {
        private readonly IDocKeyService _docKeyService = docKeyService;

        
        [HttpGet]
        public async Task<IEnumerable<DocKey>> GetDocKeys()
        {
            return await _docKeyService.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DocKey>> GetDocKey(int id)
        {
            var docKey = await _docKeyService.Get(id);

            if (docKey == null)
            {
                return NotFound();
            }

            return docKey;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutDocKey(int id, DocKey docKey)
        {
            if (id != docKey.Id)
            {
                return BadRequest();
            }

            await _docKeyService.UpdateDocKey(id, docKey);

            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult<DocKey>> PostDocKey(DocKey docKey)
        {
            await _docKeyService.CreateDocKey(docKey);

            return CreatedAtAction("GetDocKey", new { id = docKey.Id }, docKey);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDocKey(int id)
        {
            var docKey = await _docKeyService.DeleteDocKey(id);
            if (!docKey)
            {
                return NotFound();
            }
            return NoContent();
        }

    }
}
