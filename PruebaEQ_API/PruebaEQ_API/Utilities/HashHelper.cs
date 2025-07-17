using System.Security.Cryptography;
using System.Text;

namespace PruebaEQ_API.Utilities
{
    // Clase utilitaria que permite generar el hash SHA256 de una cadena de texto.
    // Se usa para validar contraseñas sin guardar el texto plano en la base de datos.

    public static class HashHelper
    {
        public static string CalculateHash(string input)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(input);                     // Convierte el string a bytes
            var hashBytes = sha256.ComputeHash(bytes);                     // Calcula el hash en SHA256
            return Convert.ToBase64String(hashBytes);                      // Lo devuelve como texto base64
        }
    }

}
