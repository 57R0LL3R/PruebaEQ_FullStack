using System.ComponentModel.DataAnnotations;

namespace PruebaEQ_API.Models
{
    public class LogProcces
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string OriginalFileName { get; set; }

        [StringLength(200)]
        public string Status { get; set; }
        [StringLength(200)]
        public string? NewFileName { get; set; } = null;
        [Required]
        public DateTime DateProcces { get; set; } = DateTime.Now;
        

    }
}
