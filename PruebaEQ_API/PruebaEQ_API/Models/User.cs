using System.ComponentModel.DataAnnotations;

namespace PruebaEQ_API.Models
{
    // Usuario del sistema que puede autenticarse mediante correo y contraseña (hasheada).

    public class User
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(100)]
        [Required]
        public string EmailAddress { get; set; }      // Correo electrónico del usuario

        [Required]
        [MaxLength(256)]
        public string ContrasenaHash { get; set; }    // Hash de la contraseña (no se guarda la contraseña en texto plano)
    }

}
