using Microsoft.AspNetCore.Mvc;
using PruebaEQ_API.Models;

namespace PruebaEQ_API.Services.Interfaces
{
    public interface ILogProccesService
    {
        public  Task<ActionResult<IEnumerable<LogProcces>>> GetAll();
        public  Task<LogProcces?> Get(int id);
        public  Task<LogProcces?> CreateLogProcces(LogProcces logProcces);
    }
}
