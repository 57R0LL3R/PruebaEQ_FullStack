using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PruebaEQ_API.Controllers
{
    // Controlador encargado de recibir archivos PDF por medio de un formulario.
    // Requiere autenticación JWT. Guarda los archivos en una ruta fija del servidor.

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PdfController(IWebHostEnvironment env) : ControllerBase
    {
        private readonly IWebHostEnvironment _env = env;

        // Endpoint para cargar un archivo PDF.
        // La ruta se ignora de la documentación Swagger por ser uso interno.
        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpPost]
        public async Task<IActionResult> UploadPdf([FromForm] IFormFile file)
        {
            // Validaciones básicas del archivo
            if (file == null || file.Length == 0)
                return BadRequest("No se envió ningún archivo.");

            if (!file.FileName.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
                return BadRequest("Solo se permiten archivos PDF.");

            if (file.Length > 10 * 1024 * 1024)
                return BadRequest("El archivo excede los 10 MB.");

            try
            {
                // Ruta donde se guardarán los archivos
                string folderPath = @"C:\PruebaEQ";
                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                // Se guarda el archivo con su nombre original
                string filePath = Path.Combine(folderPath, Path.GetFileName(file.FileName));
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                return Ok(new { mensaje = "Archivo guardado correctamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al guardar el archivo: {ex.Message}");
            }
        }
    }

}
