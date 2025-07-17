using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PruebaEQ_API.Models;
using PruebaEQ_API.Models.DTOs;
using PruebaEQ_API.Utilities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PruebaEQ_API.Controllers
{

    // Controlador responsable de la autenticación de usuarios.
    // Genera y devuelve un token JWT al iniciar sesión correctamente.
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IConfiguration config, EQContext Context) : ControllerBase
    {
        // Context para la validacion del usuario
        // Config permite cargar los datos almacenados en appsettings.json
        private readonly EQContext _context = Context;
        private readonly IConfiguration _config = config;

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDTO dto)
        {
            // Se calcula el hash de la contraseña ingresada para compararlo con el almacenado
            var hash = HashHelper.CalculateHash(dto.Contrasena);

            // Búsqueda del usuario por correo y hash en base de datos
            var user = _context.Users.FirstOrDefault(u => u.EmailAddress == dto.Usuario && u.ContrasenaHash == hash);
            if (user == null) return Unauthorized("Credenciales incorrectas");

            // Se crean los claims que se incluirán en el token (identificador del usuario y correo)
            var claims = new[]
            {
        new Claim(ClaimTypes.Name, user.EmailAddress),
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
    };

            // Configuración del token: clave secreta y algoritmo de firma
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Generación del token con duración de 3 horas
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddHours(3),
                signingCredentials: creds
            );

            // Se retorna el token generado al cliente
            return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
        }

    }
}
