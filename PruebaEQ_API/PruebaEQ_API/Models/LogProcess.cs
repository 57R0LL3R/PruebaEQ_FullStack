using System.ComponentModel.DataAnnotations;

namespace PruebaEQ_API.Models
{
    // Registro de cada proceso de carga de archivos PDF en el sistema.
    // Permite hacer seguimiento del estado de cada archivo procesado.

    public class LogProcess
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string OriginalFileName { get; set; }  // Nombre original del archivo cargado

        [StringLength(200)]
        public string Status { get; set; }            // Resultado del procesamiento

        [StringLength(200)]
        public string? NewFileName { get; set; } = null; // Nombre final del archivo procesado (si aplica)

        [Required]
        public DateTime DateProcces { get; set; } = DateTime.Now; // Fecha del procesamiento
    }

}
