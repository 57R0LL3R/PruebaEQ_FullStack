using System.ComponentModel.DataAnnotations;

namespace PruebaEQ_API.Models
{
    public class DocKey
    {
        [Key]
        int Id { get; set; }
        [Required]
        [StringLength(200)]
        string DocName { get; set; }
        [Required]
        [StringLength(200)]
        string Key { get; set; }
    }
}
