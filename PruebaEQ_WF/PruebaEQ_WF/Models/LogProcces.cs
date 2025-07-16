using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaEQ_WF.Models
{
    public class LogProcces
    {
        public int Id { get; set; }
        public string OriginalFileName { get; set; }
        public string? NewFileName { get; set; } = null;
        public string Status { get; set; } = "Unknown";
        public DateTime DateProcces { get; set; } = DateTime.Now;
    }
}
