using System.ComponentModel.DataAnnotations;

namespace PruebaEQ_API.Models
{
    // Representa una clave asociada a un documento.
    // Se usa para identificar y categorizar documentos dentro del sistema.

    public class DocKey
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string DocName { get; set; }  // Nombre del documento asociado

        [Required]
        [StringLength(200)]
        public string Key { get; set; }      // Palabra clave identificadora
    }
}
