using System.ComponentModel.DataAnnotations;

namespace PruebaEQ_API.Models
{
    public class LogProcces
    {
        [Key]
        int Id { get; set; }

        [Required]
        [StringLength(200)]
        string OriginalFileName { get; set; }

        [StringLength(200)]
        string? NewFileName { get; set; } = null;
        [Required]
        DateTime DateProcces { get; set; } = DateTime.Now;

    }
}
