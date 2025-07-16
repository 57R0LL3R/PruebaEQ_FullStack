using System.ComponentModel.DataAnnotations;

namespace PruebaEQ_API.Models
{
    public class DocKey
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(200)]
        public string DocName { get; set; }
        [Required]
        [StringLength(200)]
        public string Key { get; set; }
    }
}
